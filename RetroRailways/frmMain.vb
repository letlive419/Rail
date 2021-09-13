
'Elvis Cruz
'Mar 7,2021 
'I affirm that this program was created by me. It is solely my work and does not inlcude any work done by anybody else.
Public Class frmMain

    Private arrSeats As ArrayList
    Private ticket As frmTicket
    Public isGreen As Boolean
    Private clubCounter As Integer
    Private stanCounter As Integer





    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        grpClubSeating.Visible = False

        lstClass.Items.Add("Standard RailCar")
        lstClass.Items.Add("Club Car")

        arrSeats = New ArrayList

        arrSeats.Add(btnSeat1)
        arrSeats.Add(btnSeat2)
        arrSeats.Add(btnSeat3)
        arrSeats.Add(btnSeat4)
        arrSeats.Add(btnSeat5)
        arrSeats.Add(btnSeat6)
        arrSeats.Add(btnSeat7)
        arrSeats.Add(btnSeat8)

        clubCounter = 8
        stanCounter = 16


    End Sub



    Private Sub lstClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstClass.SelectedIndexChanged
        If lstClass.SelectedIndex = 1 Then
            grpClubSeating.Visible = True
        Else
            grpClubSeating.Visible = False

        End If
    End Sub

    Private Sub SeatSelected(sender As Object, e As EventArgs) Handles btnSeat1.Click, btnSeat2.Click, btnSeat3.Click, btnSeat4.Click, btnSeat5.Click, btnSeat6.Click, btnSeat7.Click, btnSeat8.Click
        Dim btn As Button
        btn = DirectCast(sender, Button)
        Dim i As Integer

        For i = 0 To arrSeats.Count - 1
            If arrSeats(i).enabled = True Then
                arrSeats(i).backcolor = Color.White
            End If



        Next

        btn.BackColor = Color.Green
        isGreen = True

    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearScreenControls(Me)
        lstClass.ClearSelected()
        errP.Clear()


    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim blnErrors As Boolean
        Dim num As Integer
        num = Math.Ceiling(Rnd() * 10000)
        ticket = New frmTicket
        Dim i As Integer


        errP.Clear()

        If txtName.Text = "" Then
            errP.SetError(txtName, "Please fill in the textbox with your Name!")
            blnErrors = True
        End If

        If lstClass.SelectedIndex = -1 Then
            errP.SetError(lstClass, "Please choose a class!")
            blnErrors = True
        End If

        If lstClass.SelectedItem = "Club Car" Then
            For i = 0 To arrSeats.Count - 1
                If arrSeats(i).enabled And isGreen = False Then
                    errP.SetError(grpClubSeating, "Select a Seat")
                    blnErrors = True
                End If
            Next
        End If


        If blnErrors Then
            Exit Sub
        End If

        ticket.lblName.Text = txtName.Text
        ticket.lblTime.Text = TimeOfDay
        ticket.lblClass.Text = lstClass.SelectedItem
        ticket.lblRef.Text = num.ToString

        If lstClass.SelectedItem = "Club Car" Then
            clubCounter = clubCounter - 1
            If stanCounter < 0 And clubCounter < 0 Then
                MessageBox.Show("Train is full")
                Exit Sub
            End If
        End If


        For i = 0 To arrSeats.Count - 1
            If lstClass.SelectedItem = "Club Car" Then


                If clubCounter < 0 Then
                    MessageBox.Show("All club seatings are full, please consider using the Standard Railcar if available")
                    Exit Sub

                End If
                If arrSeats(i).backcolor = Color.Green Then
                    arrSeats(i).enabled = False
                    arrSeats(i).backcolor = Color.Gray
                    ticket.lblSeat.Text = arrSeats(i).text


                End If
            Else
                ticket.lblSeat.Text = "No Seat Assignment"
            End If

        Next
        If lstClass.SelectedItem = "Standard RailCar" Then
            stanCounter = stanCounter - 1
            If stanCounter < 0 And clubCounter < 0 Then
                MessageBox.Show("Train is full")
                Exit Sub
            End If
            If stanCounter < 0 Then
                MessageBox.Show("Standard Railcar is full, please consider the Club Car if available")
                Exit Sub
            End If


        End If






        ticket.ShowDialog()

        txtName.Text = ""
        lstClass.SelectedIndex = -1
        isGreen = False
        Console.WriteLine(clubCounter)

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()


    End Sub


End Class
