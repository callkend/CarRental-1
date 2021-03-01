'Kendall Callister
'RCET 0265
'Spring 2021
'Car Rental
'

Option Explicit On
Option Strict On
Option Compare Binary

Public Class RentalForm
    Dim problemMessage(10) As String
    Dim problem As Boolean = False
    Dim numberOfProblems As Integer = 0
    Dim beginningOdometer As Integer
    Dim endingOdometer As Integer
    Dim numberOfDays As Integer
    Dim numberOfMilesDriven As Integer
    Dim mileageCharge As Double
    Dim daysCharge As Integer

    Private Sub RentalForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RentalFormToolTip.SetToolTip(NameTextBox, "Name of Customer")
        RentalFormToolTip.SetToolTip(AddressTextBox, "Address of Customer")
        RentalFormToolTip.SetToolTip(CityTextBox, "City of Residence")
        RentalFormToolTip.SetToolTip(StateTextBox, "State of Residence")
        RentalFormToolTip.SetToolTip(ZipCodeTextBox, "Zip Code of Residence")
        RentalFormToolTip.SetToolTip(BeginOdometerTextBox, "Odometer reading at start")
        RentalFormToolTip.SetToolTip(EndOdometerTextBox, "Odometer reading at end")
        RentalFormToolTip.SetToolTip(DaysTextBox, "Total Rental Days")
        RentalFormToolTip.SetToolTip(MilesradioButton, "Odometer is in Miles")
        RentalFormToolTip.SetToolTip(KilometersradioButton, "Odometer is in Kilometers")
        RentalFormToolTip.SetToolTip(TotalMilesTextBox, "Distance Driven")
        RentalFormToolTip.SetToolTip(MileageChargeTextBox, "Charge for mileage")
        RentalFormToolTip.SetToolTip(DayChargeTextBox, "Charge for days used")
        RentalFormToolTip.SetToolTip(TotalDiscountTextBox, "Amount discounted")
        RentalFormToolTip.SetToolTip(TotalChargeTextBox, "Amount due")
        RentalFormToolTip.SetToolTip(AAAcheckbox, "AAA Member Discount")
        RentalFormToolTip.SetToolTip(Seniorcheckbox, "Senior Discount")


    End Sub

    'Runs several validations of user inputs, and does calculations
    Private Sub CalculateButton_Click(sender As Object, e As EventArgs) Handles CalculateButton.Click
        'Checks for any errors in user inputs
        If Validater(NameTextBox.Text, False) = "is empty" Then
            problem = True
            problemMessage(numberOfProblems) = "Name text box is empty "
            numberOfProblems += 1
            NameTextBox.Select()
            NameTextBox.Clear()
        End If

        If Validater(AddressTextBox.Text, False) = "is empty" Then
            If problem = False Then
                AddressTextBox.Select()
            End If
            problem = True
            problemMessage(numberOfProblems) = "Address text box is empty "
            numberOfProblems += 1
            AddressTextBox.Clear()
        End If

        If Validater(CityTextBox.Text, False) = "is empty" Then
            If problem = False Then
                CityTextBox.Select()
            End If
            problem = True
            problemMessage(numberOfProblems) = "City text box is empty"
            numberOfProblems += 1
            CityTextBox.Clear()
        End If

        If Validater(StateTextBox.Text, False) = "is empty" Then
            If problem = False Then
                StateTextBox.Select()
            End If
            problem = True
            problemMessage(numberOfProblems) = "State text box is empty "
            numberOfProblems += 1
            StateTextBox.Clear()
        End If

        If Validater(ZipCodeTextBox.Text, True) = "must contain number" Then
            If problem = False Then
                ZipCodeTextBox.Select()
            End If
            problem = True
            problemMessage(numberOfProblems) = "Zip Code text box is empty "
            numberOfProblems += 1
            ZipCodeTextBox.Clear()
        End If

        If Validater(BeginOdometerTextBox.Text, True) = "must contain number" Then
            If problem = False Then
                BeginOdometerTextBox.Select()
            End If
            problem = True
            problemMessage(numberOfProblems) = "Beginning Odometer must be a number "
            numberOfProblems += 1
        Else
            beginningOdometer = CInt(BeginOdometerTextBox.Text)
        End If

        If Validater(EndOdometerTextBox.Text, True) = "must contain number" Then
            If problem = False Then
                EndOdometerTextBox.Select()
            End If
            problem = True
            problemMessage(numberOfProblems) = "Ending Odometer must be a number "
            numberOfProblems += 1
        Else
            endingOdometer = CInt(EndOdometerTextBox.Text)
        End If

        If beginningOdometer >= endingOdometer Then
            If problem = False Then
                BeginOdometerTextBox.Select()
            End If
            problem = True
            problemMessage(numberOfProblems) = "Beginning Odometer must be a less than Ending Odometer "
            numberOfProblems += 1
        End If

        If Validater(DaysTextBox.Text, True) = "must contain number" Then
            If problem = False Then
                DaysTextBox.Select()
            End If
            problem = True
            problemMessage(numberOfProblems) = "Number of Days must be a number "
            numberOfProblems += 1
        Else
            numberOfDays = CInt(DaysTextBox.Text)
        End If

        'If error occurred displays errors in message box 
        If problem = True Then
            Dim message As String = ""

            numberOfProblems -= 1
            For i = 0 To numberOfProblems
                message += $"{i + 1}." + problemMessage(i) + vbCrLf
            Next
            MessageBox.Show(message, "Errors")
            numberOfProblems = 0
            problem = False
            'If there is no error calculations are done
        Else
            'Finds miles driven
            If MilesradioButton.Checked = True Then
                numberOfMilesDriven = endingOdometer - beginningOdometer
                TotalMilesTextBox.Text = CStr(numberOfMilesDriven) + "mi"
            ElseIf KilometersradioButton.Checked = True Then
                'If input is kilometers converts to miles.
                numberOfMilesDriven = CInt((endingOdometer - beginningOdometer) * 0.62)
                TotalMilesTextBox.Text = CStr(numberOfMilesDriven) + "mi"
            End If

            'Calculates mileage charge.
            If numberOfMilesDriven <= 200 Then
                MileageChargeTextBox.Text = "0"
            Else
                numberOfMilesDriven -= 200
                If numberOfMilesDriven <= 300 Then
                    mileageCharge = CDbl(numberOfMilesDriven) * 0.12
                Else
                    mileageCharge = numberOfMilesDriven - 300 * 0.12
                    numberOfMilesDriven -= 300
                    mileageCharge += numberOfMilesDriven * 0.1
                    numberOfMilesDriven += 500
                End If
            End If

            'Displays mileage charge
            MileageChargeTextBox.Text = "$" + CStr(Math.Round(mileageCharge, 2))

            'Calculates day charge
            daysCharge = numberOfDays * 15

            'Displays day charge
            DayChargeTextBox.Text = "$" + CStr(daysCharge)

            'Calculates total discount and displays amount owed.
            If AAAcheckbox.Checked = True And Seniorcheckbox.Checked = False Then
                TotalChargeTextBox.Text = "$" + CStr(0.95 * (Math.Round(mileageCharge, 2) + daysCharge))
                TotalDiscountTextBox.Text = "$" + CStr(0.05 * (Math.Round(mileageCharge, 2) + daysCharge))
            ElseIf AAAcheckbox.Checked = True And Seniorcheckbox.Checked = True Then
                TotalChargeTextBox.Text = "$" + CStr(0.92 * (Math.Round(mileageCharge, 2) + daysCharge))
                TotalDiscountTextBox.Text = "$" + CStr(0.08 * (Math.Round(mileageCharge, 2) + daysCharge))
            ElseIf AAAcheckbox.Checked = False And Seniorcheckbox.Checked = True Then
                TotalChargeTextBox.Text = "$" + CStr(0.97 * (Math.Round(mileageCharge, 2) + daysCharge))
                TotalDiscountTextBox.Text = "$" + CStr(0.03 * (Math.Round(mileageCharge, 2) + daysCharge))
            Else
                TotalChargeTextBox.Text = "$" + CStr(1 * (Math.Round(mileageCharge, 2) + daysCharge))
                TotalDiscountTextBox.Text = "$" + CStr(0 * (Math.Round(mileageCharge, 2) + daysCharge))
            End If

            'Adds calculated data to summary
            Summary(CDbl(numberOfMilesDriven), CDbl(TotalChargeTextBox.Text.Replace("$", "")), False)
        End If
    End Sub

    'Grabs summary information
    Private Sub SummaryButton_Click(sender As Object, e As EventArgs) Handles SummaryButton.Click
        Summary(0, 0, True)
    End Sub

    'Runs clear sub
    Private Sub ClearButton_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
        Clear()
    End Sub

    'Checks to see if user inputs are valid.
    Function Validater(textBox As String, isInteger As Boolean) As String
        Dim holder As Integer
        Dim response As String
        Select Case isInteger
            Case True
                Try
                    holder = CInt(textBox)
                    response = textBox
                Catch ex As Exception
                    response = "must contain number"
                End Try
            Case False
                If textBox = "" Then
                    response = "is empty"
                Else
                    response = textBox
                End If
        End Select
        Return response
    End Function

    'Stores calculated values and displays values when called for.
    Sub Summary(distanceDriven As Double, charge As Double, read As Boolean)
        Static totalCustomers As Integer
        Static totalDistance As Double
        Static totalCharge As Double

        SummaryButton.Enabled = True

        If read = False Then
            totalCustomers += 1
            totalDistance += distanceDriven
            totalCharge += charge
            CalculateButton.Enabled = False
        Else
            MessageBox.Show($"Total Customer: {totalCustomers}" + vbCrLf +
                    $"Total Distance: {totalDistance}mi" + vbCrLf +
                    $"Total Charges: ${Math.Round(totalCharge, 2)}")
            Clear()
        End If
    End Sub

    'Clears all of the text boxes
    Sub Clear()
        NameTextBox.Clear()
        AddressTextBox.Clear()
        CityTextBox.Clear()
        StateTextBox.Clear()
        ZipCodeTextBox.Clear()
        BeginOdometerTextBox.Clear()
        EndOdometerTextBox.Clear()
        DaysTextBox.Clear()
        TotalMilesTextBox.Clear()
        MileageChargeTextBox.Clear()
        DayChargeTextBox.Clear()
        TotalDiscountTextBox.Clear()
        TotalChargeTextBox.Clear()
        AAAcheckbox.Checked = False
        Seniorcheckbox.Checked = False
        MilesradioButton.Checked = True
        CalculateButton.Enabled = True
    End Sub

    'Handles Tool Strip Menu Click
    Private Sub CalculateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CalculateToolStripMenuItem.Click
        CalculateButton.PerformClick()
    End Sub

    Private Sub ClearToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ClearToolStripMenuItem1.Click
        ClearButton.PerformClick()
    End Sub

    Private Sub SummaryToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SummaryToolStripMenuItem1.Click
        SummaryButton.PerformClick()
    End Sub

    Private Sub ExitToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem1.Click
        ExitButton.PerformClick()
    End Sub

    'Prompts user if they want to exit, and exits program if confirmed.
    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        Dim userInput As Double

        userInput = MsgBox("Do you want to exit", CType(vbYesNo + vbCritical + vbDefaultButton2, MsgBoxStyle), "Exit")
        If userInput = vbYes Then
            Me.Close()
        End If
    End Sub

End Class
