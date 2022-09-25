#pragma once

#include "MainWindow.g.h"

namespace winrt::App1::implementation {

struct MainWindow : MainWindowT<MainWindow> {
  private:
    HWND hwnd = nullptr;

  public:
    MainWindow();

    int32_t MyProperty();
    void MyProperty(int32_t value);
    Windows::Foundation::IAsyncAction DoAsync();

    void myButton_Click(Windows::Foundation::IInspectable const& sender,
                        Microsoft::UI::Xaml::RoutedEventArgs const& args);
};
} // namespace winrt::App1::implementation

namespace winrt::App1::factory_implementation {
struct MainWindow : MainWindowT<MainWindow, implementation::MainWindow> {};
} // namespace winrt::App1::factory_implementation
