using UnityEngine;
using UnityEngine.UI;
using SpeechLib;

public class TaxCalculator : MonoBehaviour
{
    // Constant rate for the Medicare Levy
    const double MEDICARE_LEVY = 0.02;
    public Dropdown TimePeriodDropdown;
    public InputField GrossSalaryInputField; 
    public enum Timeperiod
    {
        yearly = 1,
        monthly = 12,
        fortnightly = 26,
        weekly = 52,
        
    }
    
    // Variables
    bool textToSpeechEnabled = true;

    

    private void Start()
    {
        Speak("Welcome to the A.T.O. Tax Calculator");
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

        // Calculations
        double grossYearlySalary = CalculateGrossYearlySalary(grossSalaryInput, salaryPayPeriod);
        double netIncome = CalculateNetIncome(grossYearlySalary, ref medicareLevyPaid, ref incomeTaxPaid);

        // Output
        OutputResults(medicareLevyPaid, incomeTaxPaid, netIncome);
    }

    private double GetGrossSalary()
    {
        if (double.TryParse(GrossSalaryInputField.text, out double grosssalaryinput))
        {
            return grosssalaryinput;
        }
        else
        {
            return 0;
        }

    }

    private int GetSalaryPayPeriod()
    {
        // Get from user. E.g. combobox or radio buttons
        int timeperiod = TimePeriodDropdown.value;
        return timeperiod;

    }

    private double CalculateGrossYearlySalary(double grossSalaryInput, int salaryPayPeriod)
    {
        return grossSalaryInput * salaryPayPeriod;
    }

    private double CalculateNetIncome(double grossYearlySalary, ref double medicareLevyPaid, ref double incomeTaxPaid)
    {
        // This is a stub, replace with the real calculation and return the result
        medicareLevyPaid = CalculateMedicareLevy(grossYearlySalary);
        incomeTaxPaid = CalculateIncomeTax(grossYearlySalary);
        double netIncome = 33000;        
        return netIncome;
    }

    private double CalculateMedicareLevy(double grossYearlySalary)
    {  

        double medicareLevyPaid = grossYearlySalary * MEDICARE_LEVY;        
        return medicareLevyPaid;
    }

    private double CalculateIncomeTax(double grossYearlySalary)
    {
        if(grossYearlySalary == 0 && grossYearlySalary <= 18200)
        {
            return 0.00;
        }
        else if (grossYearlySalary >= 18201 && grossYearlySalary <= 37000)
        {
            return grossYearlySalary - (grossYearlySalary * 0.19);
        }
        else if (grossYearlySalary >= 37001 && grossYearlySalary <= 87000)
        {
            return grossYearlySalary - (grossYearlySalary * 0.325);
        }
        else if (grossYearlySalary >= 18201 && grossYearlySalary <= 37000)
        {
            return grossYearlySalary - (grossYearlySalary * 0.19);
        }
        else if (grossYearlySalary >= 18201 && grossYearlySalary <= 37000)
        {
            return grossYearlySalary - (grossYearlySalary * 0.19);
        }
        return 0;

    }

    private void OutputResults(double medicareLevyPaid, double incomeTaxPaid, double netIncome)
    {
        // Output the following to the GUI
        // "Medicare levy paid: $" + medicareLevyPaid.ToString("F2");
        // "Income tax paid: $" + incomeTaxPaid.ToString("F2");
        // "Net income: $" + netIncome.ToString("F2");
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
