#include "pch.h"

#include <microsoft.ui.xaml.window.h>

#include "MainWindow.xaml.h"
#if __has_include("MainWindow.g.cpp")
#include "MainWindow.g.cpp"
#endif

using namespace winrt;
using namespace Microsoft::UI::Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winrt::App1::implementation {
MainWindow::MainWindow() {
    InitializeComponent();

    // see https://learn.microsoft.com/en-us/windows/apps/develop/ui-input/retrieve-hwnd
    auto native = this->try_as<::IWindowNative>();
    winrt::check_bool(native);
    if (winrt::hresult hr = native->get_WindowHandle(&hwnd); FAILED(hr))
        winrt::throw_hresult(hr);
}

int32_t MainWindow::MyProperty() {
    throw winrt::hresult_not_implemented();
}

void MainWindow::MyProperty(int32_t /* value */) {
    throw winrt::hresult_not_implemented();
}

Windows::Foundation::IAsyncAction MainWindow::DoAsync() {
    throw winrt::hresult_not_implemented();
}

void MainWindow::myButton_Click(IInspectable const&, RoutedEventArgs const&) {
    myButton().Content(box_value(L"Clicked"));
}
} // namespace winrt::App1::implementation
