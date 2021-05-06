using UnityEngine;
using System;
using UnityEngine.UI;
using SpeechLib;

public class TaxCalculator : MonoBehaviour
{
    // Constant rate for the Medicare Levy
    const double MEDICARE_LEVY = 0.02;
    public Dropdown TimePeriodDropdown;
    public InputField GrossSalaryInputField;
    public Toggle ToggleTextTospeech; 
    public Text OutputNetIncome;
    public Text OutputMedicareLevy;
    public Text OutputTaxPaid;
    public Text Error; 
    public enum Timeperiod
    {
        yearly = 1,
        monthly = 12,
        fortnightly = 26,
        weekly = 52,
        
    }



    // Toggleing text to speech
    bool textToSpeechEnabled = false;

    public void Texttospeech()
    {
        textToSpeechEnabled = ToggleTextTospeech.isOn;
        Speak("Text to speech enabled");
    }



    // Run this function on the click event of your 'Calculate' button
    public void Calculate()
    {
        // Initialisation of variables
        double medicareLevyPaid = 0;
        double incomeTaxPaid = 0;
        // Input
        double grossSalaryInput = GetGrossSalary();
        int salaryPayPeriod = GetSalaryPayPeriod();

        if (grossSalaryInput > 0)
        {
            double grossYearlySalary = CalculateGrossYearlySalary(grossSalaryInput, salaryPayPeriod);


            double netIncome = CalculateNetIncome(grossYearlySalary, ref medicareLevyPaid, ref incomeTaxPaid);
            print(incomeTaxPaid);

            OutputResults(medicareLevyPaid, incomeTaxPaid, netIncome);
        }
        // Calculations

        // Output
        
    }

    private double GetGrossSalary()
    {
        if (double.TryParse(GrossSalaryInputField.text, out double grosssalaryinput) )
        {
            return grosssalaryinput;
        }
        
        else
        {
            GrossSalaryInputField.text = "ERROROROROOROROOROOROROOROROOROR";
            GrossSalaryInputField.enabled = false;
            return 0;
        }

    }

    private int GetSalaryPayPeriod()
    {
        // Get from user. E.g. combobox or radio buttons
        int timeperiod = TimePeriodDropdown.value;
        if(timeperiod == 0) { return 1; }
        if (timeperiod == 1) { return 12; }
        if (timeperiod == 2) { return 26; }
        if (timeperiod == 3) { return 52; }

        return 0;

    }

    private double CalculateGrossYearlySalary(double grossSalaryInput, int salaryPayPeriod)
    {
        print(grossSalaryInput);
        print(salaryPayPeriod);
        return grossSalaryInput * salaryPayPeriod;
    }

    private double CalculateNetIncome(double grossYearlySalary, ref double medicareLevyPaid, ref double incomeTaxPaid)
    {
        incomeTaxPaid = CalculateIncomeTax(grossYearlySalary);
        medicareLevyPaid = CalculateMedicareLevy(grossYearlySalary);
        double netIncome = grossYearlySalary - (medicareLevyPaid + incomeTaxPaid);  
        return netIncome;
    }

    private double CalculateMedicareLevy(double grossYearlySalary)
    {  

        double medicareLevyPaid = grossYearlySalary * MEDICARE_LEVY;        
        return medicareLevyPaid;
    }

    private double CalculateIncomeTax(double grossYearlySalary)
    {
        if(grossYearlySalary < 18000)
        {
            return 0.00;
        }
        else if (grossYearlySalary < 37000)
        {
            return  0.19 * (grossYearlySalary - 18201);
            
        }
        else if (grossYearlySalary < 87000)
        {
            return 3572 + 0.325 * (grossYearlySalary - 37001 );
        }
        else if (grossYearlySalary < 180000)
        {
            return 19822 + 0.37 * (grossYearlySalary - 87001);
        }
        else if (grossYearlySalary < 180001)
        {
            return 54232 + 0.45 * (grossYearlySalary - 180000);
        }
        else
        {
            return 0;
        }

    }

    private void OutputResults(double medicareLevyPaid, double incomeTaxPaid, double netIncome)
    {
        // Output the following to the GUI
        OutputMedicareLevy.text = "Medicare levy paid: $" + medicareLevyPaid.ToString("F2");
        
        OutputTaxPaid.text = "Income tax paid: $" + incomeTaxPaid.ToString("F2");
         
        OutputNetIncome.text = "Net income: $" + netIncome.ToString("F2");
    }

    // Text to Speech
    private void Speak(string textToSpeak)
    {
        if(textToSpeechEnabled)
        {
            SpVoice voice = new SpVoice();
            voice.Speak(textToSpeak);
        }
    }
}
