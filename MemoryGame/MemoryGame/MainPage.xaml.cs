using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MemoryGame
{
    public partial class MainPage : ContentPage
    {
        // IF WON
        public Boolean won = false;
        public int winIndex = 0;

        // RANDOM GENERATOR
        public Random rand = new Random();

        // DIFFERENT IMAGE SETS
        public List<string> images = new List<string>() 
        {
            "apple.png",
            "orange.png",
            "mango.png",
            "banana.png",
            "pineapple.png",
            "watermelon.png",
            "bomb.png",
            "tower.png",
            "apple.png",
            "orange.png",
            "mango.png",
            "banana.png",
            "pineapple.png",
            "watermelon.png",
            "bomb.png",
            "tower.png"
        };

        // IMAGE BUTTONS LIST
        public List<string> shuffledImageList;
        public List<ImageButton> buttons = new List<ImageButton>();

        // AMOUNT SELECTED
        public int selectedCount = 0;

        // SELECTED BUTTONS
        public ImageButton firstSelection = new ImageButton();
        public ImageButton secondSelection = new ImageButton();

        public MainPage()
        {
            InitializeComponent();

            buttons.Add(Button1);
            buttons.Add(Button2);
            buttons.Add(Button3);
            buttons.Add(Button4);
            buttons.Add(Button5);
            buttons.Add(Button6);
            buttons.Add(Button7);
            buttons.Add(Button8);
            buttons.Add(Button9);
            buttons.Add(Button10);
            buttons.Add(Button11);
            buttons.Add(Button12);
            buttons.Add(Button13);
            buttons.Add(Button14);
            buttons.Add(Button15);
            buttons.Add(Button16);

            shuffledImageList = images.OrderBy(image => rand.Next()).ToList();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            ImageButton pressedImage = (ImageButton)sender;

            if (pressedImage.Source == null)
            {
                pressedImage.Source = shuffledImageList[int.Parse(pressedImage.ClassId)];
            }

            if (selectedCount == 0)
            {
                firstSelection = pressedImage;
            }
            else if(selectedCount == 1)
            {
                secondSelection = pressedImage;
            }

            selectedCount++;

            if(selectedCount == 2)
            {
                LayoutGrid.IsEnabled = false;
                await Task.Delay(1000);
                LayoutGrid.IsEnabled = true;
                if (firstSelection.Source != null && secondSelection.Source != null)
                {
                    if ((firstSelection.Source.ToString() == secondSelection.Source.ToString()) && (firstSelection.ClassId != secondSelection.ClassId))
                    {
                        firstSelection.Source = "check.png";
                        secondSelection.Source = "check.png";

                        foreach (var button in buttons)
                        {
                            if(button.Source.ToString() == "check.png")
                            {
                                winIndex += 1;
                            }
                        }
                    } 
                    else
                    {
                        firstSelection.Source = null;
                        secondSelection.Source = null;
                    }
                }
                selectedCount = 0;
            }
        }
        private void ResetButton_Clicked(object sender, EventArgs e)
        {
            selectedCount = 0;
            firstSelection.Source = null;
            secondSelection.Source = null;

            shuffledImageList = images.OrderBy(image => rand.Next()).ToList();

            foreach (var button in buttons)
            {
                button.Source = null;
                button.BackgroundColor = Color.FromHex("#000000");
                button.IsEnabled = true;
            }
        }
    }
}
