' Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x407 dokumentiert.

Imports Windows.System
''' <summary>
''' Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
''' </summary>
Public NotInheritable Class MainPage
    Inherits Page

    Public Sub New()

        InitializeComponent()

        NavigationCacheMode = NavigationCacheMode.Required

        Load()

    End Sub

#Region "Variables"
    Public ReadOnly Property CurrentUser As User
#End Region

    Private Async Sub Load()
#Region "User Profile"
        Dim users = Await User.FindAllAsync()
        _CurrentUser = users.Where(Function(user) user.Type = UserType.LocalUser AndAlso user.AuthenticationStatus = UserAuthenticationStatus.LocallyAuthenticated).FirstOrDefault()
        PersonPicture.DisplayName = Await CurrentUser.GetPropertyAsync(KnownUserProperties.AccountName)

        Dim streamReference = Await CurrentUser.GetPictureAsync(UserPictureSize.Size64x64)
        If streamReference IsNot Nothing Then
            Dim profilePicture = New BitmapImage()
            profilePicture.SetSource(Await streamReference.OpenReadAsync())
            PersonPicture.ProfilePicture = profilePicture
        End If

        ProfileNameTextBlock.Text = $"{Await CurrentUser.GetPropertyAsync(KnownUserProperties.FirstName)} {Await CurrentUser.GetPropertyAsync(KnownUserProperties.LastName)}"
#End Region

        MainNavigationView.SelectedItem = MainNavigationView.MenuItems(0)
    End Sub

    Private Sub MainNavigationView_SelectionChanged(sender As NavigationView, args As NavigationViewSelectionChangedEventArgs)

        Select Case DirectCast(sender.SelectedItem, NavigationViewItem).Tag?.ToString().ToLower()
            Case "beamer"
                MainContentFrame.Navigate(GetType(Frames.BeamerFrame))

            Case "hotspot"
                MainContentFrame.Navigate(GetType(Frames.HotSpotFrame))

            Case "cast_receiver"
                MainContentFrame.Navigate(GetType(Frames.CastReceiverFrame))

            Case Else
                MainContentFrame.Navigate(GetType(Frames.HomeFrame))
        End Select

    End Sub

    Private Sub AccountNavigationViewItem_Tapped(sender As Object, e As TappedRoutedEventArgs)
        Launcher.LaunchUriAsync(New Uri("ms-settings:yourinfo"))
    End Sub
End Class
