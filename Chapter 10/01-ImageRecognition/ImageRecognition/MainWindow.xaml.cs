using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.AI.MachineLearning;
using Windows.Graphics.Imaging;
using Windows.Media;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;

namespace ImageRecognition
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private async Task<LearningModelSession> CreateLearningSession()
        {
            var modelFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///squeezenet1.1-7.onnx"));

            LearningModel model = await LearningModel.LoadFromStorageFileAsync(modelFile);
            LearningModelSession session = new LearningModelSession(model);
            return session;
        }

        private async Task<Dictionary<long, string>> LoadLabels()
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///sysnet.txt"));
            var text = await FileIO.ReadTextAsync(file);
            var labels = new Dictionary<long, string>();
            var records = text.Split(Environment.NewLine);
            foreach (var record in records)
            {
                var fields = record.Split(",", 2);
                if (fields.Length == 2)
                {
                    var index = long.Parse(fields[0]);
                    labels[index] = fields[1];
                }
            }
            return labels;
        }

        private async Task<ImageFeatureValue> PickImageAsync()
        {
            var filePicker = new FileOpenPicker();

            // Get the current window's HWND by passing in the Window object
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

            // Associate the HWND with the file picker
            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hwnd);

            SoftwareBitmap softwareBitmap;

            filePicker.FileTypeFilter.Add("*");
            var file = await filePicker.PickSingleFileAsync();

            using (IRandomAccessStream accessStream = await file.OpenAsync(FileAccessMode.Read))
            {
                // Create the decoder from the stream 
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(accessStream);

                // Get the SoftwareBitmap representation of the file in BGRA8 format
                softwareBitmap = await decoder.GetSoftwareBitmapAsync();
                softwareBitmap = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);

                BitmapImage preview = new BitmapImage();
                await preview.SetSourceAsync(accessStream);
                imgPreview.Source = preview;
            }

            // Encapsulate the image in the WinML image type (VideoFrame) to be bound and evaluated
            VideoFrame input = VideoFrame.CreateWithSoftwareBitmap(softwareBitmap);
            ImageFeatureValue image = ImageFeatureValue.CreateFromVideoFrame(input);

            return image;
        }

        private async Task<IReadOnlyList<float>> EvaluateModelAsync(LearningModelSession session, ImageFeatureValue image)
        {
            LearningModelBinding bind = new LearningModelBinding(session);

            bind.Bind("data", image);

            var results = await session.EvaluateAsync(bind, "0");

            // Retrieve the results of evaluation
            var resultTensor = results.Outputs["squeezenet0_flatten0_reshape0"] as TensorFloat;
            var resultVector = resultTensor.GetAsVectorView();

            return resultVector;
        }

        private List<(int index, float probability)> SortItems(IReadOnlyList<float> resultVector)
        {
            // Find the top 3 probabilities
            List<(int index, float probability)> indexedResults = new List<(int, float)>();

            for (int i = 0; i < resultVector.Count; i++)
            {
                indexedResults.Add((index: i, probability: resultVector.ElementAt(i)));
            }

            // Sort the results in order of highest probability
            indexedResults.Sort((a, b) =>
            {
                if (a.probability < b.probability)
                {
                    return 1;
                }
                else if (a.probability > b.probability)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            });

            return indexedResults;
        }

        private async void OnRecognize(object sender, RoutedEventArgs e)
        {
            LearningModelSession session = await CreateLearningSession();

            Dictionary<long, string> labels = await LoadLabels();

            ImageFeatureValue image = await PickImageAsync();

            IReadOnlyList<float> resultVector = await EvaluateModelAsync(session, image);

            List<(int index, float probability)> indexedResults = SortItems(resultVector);

            lstPredictions.Items.Clear();

            // Display the results
            for (int i = 0; i < 3; i++)
            {
                lstPredictions.Items.Add($"\"{labels[indexedResults[i].index]}\" with confidence of {indexedResults[i].probability}");
            }
        }
    }
}
