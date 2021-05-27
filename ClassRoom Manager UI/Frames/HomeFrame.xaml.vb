
Imports ClassRoom_Manager.UI.Interop
Imports Windows.System

Namespace Frames

    Public NotInheritable Class HomeFrame
        Inherits Page

        Public Sub New()

            InitializeComponent()
            Load()

        End Sub

        Public Async Sub Load()
            Dim users = Await User.FindAllAsync()
            Dim CurrentUser = users.Where(Function(user) user.Type = UserType.LocalUser AndAlso user.AuthenticationStatus = UserAuthenticationStatus.LocallyAuthenticated).FirstOrDefault()
            PersonPicture.DisplayName = Await CurrentUser.GetPropertyAsync(KnownUserProperties.AccountName)

            Dim streamReference = Await CurrentUser.GetPictureAsync(UserPictureSize.Size208x208)
            If streamReference IsNot Nothing Then
                Dim profilePicture = New BitmapImage()
                profilePicture.SetSource(Await streamReference.OpenReadAsync())
                PersonPicture.ProfilePicture = profilePicture
            End If

            ProfileNameTextBlock.Text = $"{Await CurrentUser.GetPropertyAsync(KnownUserProperties.FirstName)} {Await CurrentUser.GetPropertyAsync(KnownUserProperties.LastName)}"
        End Sub

        Private Sub ToolPresenter_Click(sender As Object, e As RoutedEventArgs)
            Launcher.LaunchUriAsync(New Uri("ms-settings:display"))
        End Sub

        Private Sub ToolPresenter_Click_1(sender As Object, e As RoutedEventArgs)
            Task.Run(Sub()
                         Wow64Interop.RunWithoutRedirect(Sub()
                                                             Process.Start(New ProcessStartInfo() With {
                                                                   .FileName = "C:\Windows\System32\DisplaySwitch.exe",
                                                                   .UseShellExecute = False
                                                               })
                                                         End Sub)
                     End Sub)
        End Sub
    End Class

End Namespace