Imports Windows.System

Namespace Frames

    Public NotInheritable Class HomeFrame
        Inherits Page

        Public Sub New()

            InitializeComponent()
            NavigationCacheMode = NavigationCacheMode.Required

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
    End Class

End Namespace