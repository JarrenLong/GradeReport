/*
 * SkillsUSA National Championships 2012
 * Contestant #510
 * Program #2 - Grade Report
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace GradeReport
{
    public partial class MainPage : PhoneApplicationPage
    {
        private int[] grade = { 90, 80, 70, 60 }; //Our grade ranges for testing
        private List<KeyValuePair<string, string>> mCourseCodes; // This will hold our course codes

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            // Create the course code list
            mCourseCodes = new List<KeyValuePair<string, string>>();
            mCourseCodes.Add(new KeyValuePair<string, string>("M", "Math"));
            mCourseCodes.Add(new KeyValuePair<string, string>("E", "English"));
            mCourseCodes.Add(new KeyValuePair<string, string>("G", "Gym"));
            mCourseCodes.Add(new KeyValuePair<string, string>("H", "History"));
        }

        // Calculate the letter grade based in the decimal score inputted
        private string getLetterGrade(decimal score)
        {
            // Keep looping until we run out of test cases or find
            // the right grade range in the array.
            int i = 0;
            while (i < grade.Length && score < grade[i])
                i++;
            return i != 4 ? ((char)(65 + i)).ToString() : "F";
        }

        //Convert a string into a course name using the list of KVPs built in the constructor
        private string getCourseCode(string code)
        {
            foreach (KeyValuePair<string, string> kvp in mCourseCodes)
            {
                // If the first letters match, we found the course! Return it.
                if (kvp.Key[0] == code.ToUpper()[0])
                {
                    return kvp.Value;
                }
            }
            return "Unknown";
        }

        private void buttonCalculate_Click(object sender, RoutedEventArgs e)
        {
            // Grab the score that the user input and convert it to a decimal
            decimal score = 0;
            bool showResults = true;
            try
            {
                score = Convert.ToDecimal(textBoxNumberGrade.Text);
            }
            catch
            {
                // If we can't convert it, throw an error
                showResults = false;
                MessageBox.Show("Please enter your score");
            }

            // Make sure the user entered a course number
            if (textBoxCourse.Text.Length < 1)
            {
                showResults = false;
                MessageBox.Show("Please enter the course code (M, E, G, or H)");
            }

            // Judges: I'm intentionally not validating the student's name or ID.
            // It seems that this is unnecessary information that plays no part in
            // the processing being performed by this application, and is only there
            // for identification purposes. Some people prefer anonymitity...

            // If we can show results, please do so
            if (showResults)
            {
                // Set the text results
                textBlockGrade.Text = "Hi " + textBoxName.Text + "(" + textBoxSID.Text +
                    "),  you received an " + getLetterGrade(score) + " (" +
                    textBoxNumberGrade.Text + ") in your " + getCourseCode(textBoxCourse.Text) + " class!";
            }
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear out the textbox that got focus
            ((TextBox)sender).Text = "";
        }
    }
}
