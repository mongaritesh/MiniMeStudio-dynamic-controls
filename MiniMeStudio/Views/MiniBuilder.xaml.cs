using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Blob;
using MiniMeStudio.Helpers;
using MiniMeStudio.Models;
using MiniMeStudio.Services;
using MSHTML;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static MiniMeStudio.Models.SaveDraft;

namespace MiniMeStudio.Views
{
    /// <summary>
    /// Interaction logic for MiniBuilder.xaml
    /// </summary>
    public partial class MiniBuilder : Window
    {
        // requires using Microsoft.Extensions.Configuration;
        public static int buttonCount = 1;
        public IHTMLDocument3 websiteDocument;
        public HTMLDocumentEvents2_onmousemoveEventHandler _dwMouseMoveEventHandler;

        public HTMLDocumentEvents2_onmouseoutEventHandler _dwMouseOutEventHandler;
        public HTMLDocumentEvents2_onclickEventHandler _dwSelectedElementOnClickEventHandler;
        public delegate void DHTMLEvent(IHTMLEventObj e);
        public Button curentButton;
        private AdornerLayer myAdornerLayer;

        private Control _currentControl;
        private string ControlSelected = string.Empty;
        SaveDraft.MiniMain SaveDraft = new SaveDraft.MiniMain();
        public MiniBuilder()
        {
            InitializeComponent();
            wbTargetURL.Navigate(Globals.DefaultWebURL);
            txtWebsiteURL.Text = Globals.DefaultWebURL;
            txtWebsiteURL.Loaded += TxtWebsiteURL_Loaded;
            MiniStackPanel.MouseEnter += MiniStackPanel_MouseEnter;
            MiniStackPanel.MouseLeave += MiniStackPanel_MouseLeave;
        }

        private void MiniStackPanel_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void MiniStackPanel_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        #region "Enums"

        public enum InputControlType
        {
            None,
            checkbox,
            Textbox,
            Dropdown,
            Button,
            Image,
            Number,
            Password,
            text,
            submit,
            TextBlock
        }


        #endregion


        #region "Web Browser"

        private void TxtWebsiteURL_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void wbTargetURL_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //MessageBox.Show("Navigate");
            //txtWebsiteURL.Text = e.Uri.OriginalString;
        }

        private void txtWebsiteURL_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                wbTargetURL.Navigate(txtWebsiteURL.Text);
        }


        private bool IEvent_oncontrolselect(IHTMLEventObj e)
        {
            e.srcElement.style.border = "3px solid Blue";
            e.srcElement.setAttribute("focus", true, 1);
            return true;
        }

        //private void IEvent_onmouseout(IHTMLEventObj pEvtObj)
        //{
        //    throw new NotImplementedException();
        //}

        //private void IEvent_onmouseover(IHTMLEventObj pEvtObj)
        //{
        //    //throw new NotImplementedException();
        //}

        //private void _dwMouseMoveEventHandler(IHTMLEventObj pEvtObj)
        //{
        //    throw new NotImplementedException();
        //}

        private void MouseMoveEventHandler(IHTMLEventObj e)
        {
            if (e.srcElement.getAttribute("id") != null)
            {
                if (_dwSelectedElementOnClickEventHandler != null)
                {
                    e.srcElement.onclick = null;
                    _dwSelectedElementOnClickEventHandler = null;
                }
                _dwSelectedElementOnClickEventHandler = new HTMLDocumentEvents2_onclickEventHandler(SelectedElementOnClickEventHandler);


                //DHTMLEventHandler myHandler = new DHTMLEventHandler(framedoc);
                //myHandler.Handler += new DHTMLEvent(this.BrowserEventHandler);

                var x = websiteDocument.getElementById(e.srcElement.id);
                InputControlType controlType = new InputControlType();
                // x.onclick = _dwSelectedElementOnClickEventHandler;
                if (x.tagName == "INPUT")
                {
                    var type = x.getAttribute("type");
                    Enum.TryParse<InputControlType>(type.ToString(), out controlType);
                    switch (controlType)
                    {
                        case InputControlType.None:
                            break;
                        case InputControlType.checkbox:
                            break;
                        case InputControlType.Textbox:
                            break;
                        case InputControlType.Dropdown:
                            break;
                        case InputControlType.Button:
                            break;
                        case InputControlType.Image:
                            break;
                        case InputControlType.Number:
                            break;
                        case InputControlType.Password:
                            break;
                        case InputControlType.text:
                            break;
                        case InputControlType.submit:
                            break;
                        default:
                            break;
                    }
                    e.srcElement.style.border = "3px solid Blue";
                }


                ToolTip toolTip = new ToolTip();
                if (x.tagName == "LABEL")
                {
                    if (ControlSelected == "LABEL")
                    {
                        e.srcElement.style.border = "3px solid Green";

                        
                    }


                }


                if (ControlSelected == "LABEL" && x.tagName == "LABEL")
                {
                    toolTip.Content = "Click to add " + ControlSelected;
                    toolTip.StaysOpen = true;
                    toolTip.IsOpen = true;
                }
                else if (ControlSelected == "BUTTON" && (controlType.ToString().ToUpper() == "BUTTON" || controlType.ToString().ToUpper() == "SUBMIT"))
                {
                    toolTip.Content = "Click to add " + ControlSelected;
                    toolTip.StaysOpen = true;
                    toolTip.IsOpen = true;
                }
                else if (ControlSelected == "TEXT" && controlType.ToString().ToUpper() == "TEXT")
                {
                    toolTip.Content = "Click to add " + ControlSelected;
                    toolTip.StaysOpen = true;
                    toolTip.IsOpen = true;

                }
                else
                {
                    toolTip.Content = "Try selecting a " + ControlSelected;
                    toolTip.StaysOpen = true;
                    toolTip.IsOpen = true;
                }


                
                toolTip.IsOpen = false;
                //e.srcElement.setAttribute("focus", true, 1);
            }
        }

        private void MouseOutEventHandler(IHTMLEventObj e)
        {
            e.srcElement.style.border = string.Empty;

        }


        private void MouseMoveEventHandler(object sender, RoutedEventArgs e)
        {

        }

        private bool SelectedElementOnClickEventHandler(IHTMLEventObj e)
        {
            return false;
        }


        #endregion

        #region "Ribbon Events"
        private void Text_Click(object sender, RoutedEventArgs e)
        {
            //AddLabel();
            ControlSelected = "LABEL";
            //AddPrimaryButton();

            //MessageBox.Show("Text Button clicked");

            //var uri = await Utility.GetBlobSharedAccessTokenAsync();
            //var blobURI = new Uri(uri);
            //var cloudBlobContainer = new CloudBlobContainer(blobURI);
            //var blockBlob = cloudBlobContainer.GetBlockBlobReference("testBlob.txt");
            //await blockBlob.UploadTextAsync("Hello world");

        }

        private void TextPair_Click(object sender, RoutedEventArgs e)
        {
            //AddSecondaryButton();
            //AddTextBox();
            ControlSelected = "TEXT";
        }

        private void BtnTemp_Click(object sender, RoutedEventArgs e)
        {

            _propertyGrid.SelectedObject = e.Source;

            //MessageBox.Show(((ContentControl)sender).Content + " clicked", Title = "MiniMe Message");
            //Get the value of the name attribute for "ElementId"
            //wbTargetURL.BringIntoView();
            //dynamic doc_obj = wbTargetURL.Document;

            //string htmlText = doc_obj.documentElement.InnerHtml;
        }

        private void PhoneNumber_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(((ContentControl)sender).Content + " Button clicked", Title = "MiniMe Message");
            websiteDocument = (IHTMLDocument3)wbTargetURL.Document;
            var test = websiteDocument.getElementById("TxtGroupid");
            test.innerText = "000";

            var client = websiteDocument.getElementById("TxtClientid");
            client.innerText = "000";

            HTMLDocumentEvents2_Event iEvent; //Event in the mshtml Document through which mouse events can be raised.

            iEvent = (HTMLDocumentEvents2_Event)websiteDocument;
            //iEvent.onclick += new mshtml.HTMLDocumentEvents2_onclickEventHandler(ClickEventHandler);
            //iEvent.onmousemove = 
            //iEvent.onmouseover += IEvent_onmouseover;
            if (_dwMouseMoveEventHandler != null)
                iEvent.onmousemove -= _dwMouseMoveEventHandler;
            _dwMouseMoveEventHandler = new HTMLDocumentEvents2_onmousemoveEventHandler(MouseMoveEventHandler);

            iEvent.onmousemove += _dwMouseMoveEventHandler;

            if (_dwMouseOutEventHandler != null)
                iEvent.onmouseout -= _dwMouseOutEventHandler;
            _dwMouseOutEventHandler = new HTMLDocumentEvents2_onmouseoutEventHandler(MouseOutEventHandler);
            iEvent.onmouseout += _dwMouseOutEventHandler;

            iEvent.oncontrolselect += IEvent_oncontrolselect;

            iEvent.onclick += IEvent_onclick;
        }

        private bool IEvent_onclick(IHTMLEventObj pEvtObj)
        {

            var x = pEvtObj.srcElement.getAttribute("id");
            if (pEvtObj.srcElement.id != null)
            {
                var xy = websiteDocument.getElementById(pEvtObj.srcElement.id);

                switch (xy.tagName)
                {
                    case "LABEL":
                        if (ControlSelected == "LABEL")
                        {
                            AddLabel(xy);
                        }
                        
                        break;
                    case "INPUT":

                        var type = xy.getAttribute("type");
                        Enum.TryParse<InputControlType>(type.ToString(), out InputControlType controlType);
                        switch (controlType)
                        {
                            case InputControlType.None:
                                break;
                            case InputControlType.checkbox:
                                break;
                            case InputControlType.Textbox:
                                break;
                            case InputControlType.Dropdown:
                                break;
                            case InputControlType.Button:
                                break;
                            case InputControlType.Image:
                                break;
                            case InputControlType.Number:
                                break;
                            case InputControlType.Password:
                                break;
                            case InputControlType.text:
                                if (ControlSelected == "TEXT")
                                {
                                    AddTextBox(xy);

                                }
                                break;
                            case InputControlType.submit:
                                if (ControlSelected == "BUTTON" || ControlSelected == "SUBMIT")
                                {
                                    AddSecondaryButton(xy);
                                }
                                break;
                            default:
                                break;
                        }


                        break;
                    default:
                        break;
                }


            }


            return true;
        }

        private void EmailAddress_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(((ContentControl)sender).Content + " Button clicked");
        }

        private void Address_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(((ContentControl)sender).Content + " Button clicked");

        }

        private void Table_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(((ContentControl)sender).Content + " Button clicked");
        }

        private void Lists_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(((ContentControl)sender).Content + " Button clicked");
        }

        private void Notifications_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(((ContentControl)sender).Content + " Button clicked");
        }

        private void DataEnhancements_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(((ContentControl)sender).Content + " Button clicked");
        }

        private void Quickpick_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(((ContentControl)sender).Content + " Button clicked");

        }

        private void TextLink_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(((ContentControl)sender).Content + " Button clicked");
        }

        private void Menu1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PopUpMenu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextFieldAutoComplete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Dropdown_Click(object sender, RoutedEventArgs e)
        {
            var btnTemp = new ListBox
            {
                Name = "ListBox" + Convert.ToString(buttonCount),
                //Content = "Button" + Convert.ToString(buttonCount),
                Width = 110,
                Height = 30,
                Margin = new Thickness(5),
                Padding = new Thickness(3),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            btnTemp.MouseDoubleClick += BtnTemp_Click;
            btnTemp.MouseEnter += BtnTemp_MouseEnter;
            btnTemp.MouseLeave += BtnTemp_MouseLeave;
            MiniStackPanel.Children.Add(btnTemp);
            _propertyGrid.SelectedObject = btnTemp;
            buttonCount++;
        }

        private void Datepicker_Click(object sender, RoutedEventArgs e)
        {
            var btnTemp = new DatePicker
            {
                Name = "DatePicker" + Convert.ToString(buttonCount),
                //Content = "Button" + Convert.ToString(buttonCount),
                Width = 110,
                Height = 30,
                Margin = new Thickness(5),
                Padding = new Thickness(3),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                SelectedDate = DateTime.Now
            };
            btnTemp.MouseDoubleClick += BtnTemp_Click;
            btnTemp.MouseEnter += BtnTemp_MouseEnter;
            btnTemp.MouseLeave += BtnTemp_MouseLeave;
            MiniStackPanel.Children.Add(btnTemp);
            _propertyGrid.SelectedObject = btnTemp;
            buttonCount++;

        }

        private void MultiSelect_Click(object sender, RoutedEventArgs e)
        {
            var btnTemp = new ListBox
            {
                Name = "MultiSelect" + Convert.ToString(buttonCount),
                //Content = "Button" + Convert.ToString(buttonCount),
                Width = 110,
                Height = 30,
                SelectionMode = SelectionMode.Multiple,

                Margin = new Thickness(5),
                Padding = new Thickness(3),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            btnTemp.MouseDoubleClick += BtnTemp_Click;
            btnTemp.MouseEnter += BtnTemp_MouseEnter;
            btnTemp.MouseLeave += BtnTemp_MouseLeave;
            MiniStackPanel.Children.Add(btnTemp);
            _propertyGrid.SelectedObject = btnTemp;
            buttonCount++;
        }

        private void Checkbox_Click(object sender, RoutedEventArgs e)
        {
            var btnTemp = new CheckBox
            {
                Name = "CheckBox" + Convert.ToString(buttonCount),
                Content = "CheckBox" + Convert.ToString(buttonCount),
                Width = 110,
                Height = 30,
                ClickMode = ClickMode.Press,
                Margin = new Thickness(5),
                Padding = new Thickness(3),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            btnTemp.MouseDoubleClick += BtnTemp_Click;
            btnTemp.MouseEnter += BtnTemp_MouseEnter;
            btnTemp.MouseLeave += BtnTemp_MouseLeave;
            MiniStackPanel.Children.Add(btnTemp);
            _propertyGrid.SelectedObject = btnTemp;
            buttonCount++;
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            var btnTemp = new RadioButton
            {
                Name = "Radio" + Convert.ToString(buttonCount),
                Content = "Radio" + Convert.ToString(buttonCount),
                Width = 110,
                Height = 30,
                ClickMode = ClickMode.Press,
                Margin = new Thickness(5),
                Padding = new Thickness(3),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            btnTemp.Click += BtnTemp_Click;
            btnTemp.MouseEnter += BtnTemp_MouseEnter;
            btnTemp.MouseLeave += BtnTemp_MouseLeave;
            MiniStackPanel.Children.Add(btnTemp);
            _propertyGrid.SelectedObject = btnTemp;
            buttonCount++;
        }

        private void FileUpload_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LookUp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ControlSelected = "BUTTON";
            //AddPrimaryButton();
        }

        private void AddImage_Click(object sender, RoutedEventArgs e)
        {

        }


        #endregion


        #region "MobileAppActions"
        private void Links_Click(object sender, RoutedEventArgs e)
        {
            _propertyGrid.SelectedObject = e.Source;
        }

        private void Pages_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Import_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddPage_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region "Functions"


        private void AddLabel()
        {
            var btnTemp = new TextBlock
            {
                Text = "Label" + Convert.ToString(buttonCount),
                Style = FindResource("MediumIconStyle") as Style,
                Width = 100,
                Height = 40,
                Tag = "Label" + Convert.ToString(buttonCount)

            };
            btnTemp.MouseEnter += BtnTemp_Click;
            btnTemp.MouseEnter += BtnTemp_MouseEnter;
            btnTemp.MouseLeave += BtnTemp_MouseLeave;
            MiniStackPanel.Children.Add(btnTemp);
            buttonCount++;
            //AddTextBox();
        }


        private void AddTextBox(IHTMLElement xy)
        {
            var btnTemp = new TextBox
            {
                Text = xy.getAttribute("Value") == null ? "" : xy.getAttribute("Value").ToString(),
               
                Width = 100,
                Height = 40,
                Name = xy.id,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontSize = 18,
                Tag = Text
            };
            btnTemp.MouseEnter += BtnTemp_Click;
            btnTemp.MouseEnter += BtnTemp_MouseEnter;
            btnTemp.MouseLeave += BtnTemp_MouseLeave;
            MiniStackPanel.Children.Add(btnTemp);
            buttonCount++;
            
        }

        private void AddLabel(IHTMLElement xy)
        {
            var btnTemp = new TextBlock
            {
                Text = xy.innerText,
                Style = FindResource("MediumIconStyle") as Style,
                Width = 100,
                Height = 40,
                Name = xy.id,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontSize = 18,
                Tag = xy.innerText

            };
            btnTemp.MouseEnter += BtnTemp_Click;
            btnTemp.MouseEnter += BtnTemp_MouseEnter;
            btnTemp.MouseLeave += BtnTemp_MouseLeave;
            MiniStackPanel.Children.Add(btnTemp);
            buttonCount++;
        }



        private void AddPrimaryButton()
        {
            var btnTemp = new Button
            {
                Name = "Button" + Convert.ToString(buttonCount),
                Content = "Button" + Convert.ToString(buttonCount),
                Style = FindResource("PrimaryRoundedButton") as Style,
                Width = 110,
                Height = 40
            };
            btnTemp.Click += BtnTemp_Click;
            btnTemp.MouseEnter += BtnTemp_MouseEnter;
            btnTemp.MouseLeave += BtnTemp_MouseLeave;
            MiniStackPanel.Children.Add(btnTemp);
            buttonCount++;
        }

        private void AddSecondaryButton()
        {
            var btnTemp = new Button
            {
                Name = "Button" + Convert.ToString(buttonCount),
                Content = "Button" + Convert.ToString(buttonCount),
                Style = FindResource("SecondaryRoundedButton") as Style,
                Width = 110,
                Height = 40
            };
            btnTemp.Click += BtnTemp_Click;
            btnTemp.MouseEnter += BtnTemp_MouseEnter;
            btnTemp.MouseLeave += BtnTemp_MouseLeave;
            MiniStackPanel.Children.Add(btnTemp);
            buttonCount++;
        }

        private void AddSecondaryButton(IHTMLElement xy)
        {
            var btnTemp = new Button
            {
                Name = xy.innerHTML,
                Content = xy.getAttribute("Value"),
                Style = FindResource("SecondaryRoundedButton") as Style,
                Width = 110,
                Height = 40,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontSize = 18
                

            };
            btnTemp.Click += BtnTemp_Click;
            btnTemp.MouseEnter += BtnTemp_MouseEnter;
            btnTemp.MouseLeave += BtnTemp_MouseLeave;
            MiniStackPanel.Children.Add(btnTemp);
            buttonCount++;
        }

        private void BtnTemp_MouseLeave(object sender, MouseEventArgs e)
        {
            var selectedItem = e.Source as UIElement;

            Adorner[] toRemoveArray = myAdornerLayer.GetAdorners(selectedItem);
            if (toRemoveArray != null)
            {
                for (int x = 0; x < toRemoveArray.Length; x++)
                {
                    myAdornerLayer.Remove(toRemoveArray[x]);
                }
            }
        }

        private void BtnTemp_MouseEnter(object sender, MouseEventArgs e)
        {
            var selectedItem = e.Source as UIElement;
            myAdornerLayer = AdornerLayer.GetAdornerLayer(selectedItem);
            //myAdornerLayer.Add(new SimpleCircleAdorner(selectedItem));
            myAdornerLayer.Add(new ResizingAdorner(selectedItem));


            //var type = selectedItem.GetType();
            //Enum.TryParse<InputControlType>(type.Name, out InputControlType controlType);
            //var controlElement = GetControlElement(controlType, sender);

            //// If a ContentElement.
            //if (controlElement != null)
            //{
            //    controlElement.Foreground = Brushes.DarkBlue;
            //    controlElement.FontSize = 10;
            //    controlElement.FontWeight = FontWeights.ExtraBold;
            //    controlElement.BorderBrush = Brushes.Blue;
            //    controlElement.BorderThickness = new Thickness(1);
            //    // Fields used to reset the UI when the mouse 
            //    // button is released.
            //    //_focusPredicted = true;
            //    //_predictedControl = controlElement;
            //}
            //Button button = sender as Button;
            //button.Background = Brushes.White;
            //button.Background = Brushes.Gray;


            //// Create a NameScope for this page so that
            //// Storyboards can be used.
            //NameScope.SetNameScope(this, new NameScope());

            //// Create a Border which will be the target of the animation.
            //Border myBorder = new Border();
            //myBorder.Background = Brushes.LightBlue;
            //myBorder.BorderBrush = Brushes.Blue;
            //myBorder.BorderThickness = new Thickness(1);
            //myBorder.Margin = new Thickness(2);
            //myBorder.Padding = new Thickness(2);

            //// Assign the border a name so that
            //// it can be targeted by a Storyboard.
            //this.RegisterName(
            //    "myAnimatedBorder", myBorder);

            //ThicknessAnimation myThicknessAnimation = new ThicknessAnimation();
            //myThicknessAnimation.Duration = TimeSpan.FromSeconds(1.5);
            //myThicknessAnimation.FillBehavior = FillBehavior.HoldEnd;

            //// Set the From and To properties of the animation.
            //// BorderThickness animates from left=1, right=1, top=1, and bottom=1
            //// to left=28, right=28, top=14, and bottom=14 over one and a half seconds.
            //myThicknessAnimation.From = new Thickness(1, 1, 1, 1);
            //myThicknessAnimation.To = new Thickness(28, 14, 28, 14);

            //// Set the animation to target the Size property
            //// of the object named "myArcSegment."
            //Storyboard.SetTargetName(myThicknessAnimation, "myAnimatedBorder");
            //Storyboard.SetTargetProperty(
            //    myThicknessAnimation, new PropertyPath(Border.BorderThicknessProperty));

            //// Create a storyboard to apply the animation.
            //Storyboard ellipseStoryboard = new Storyboard();
            //ellipseStoryboard.Children.Add(myThicknessAnimation);

            //// Start the storyboard when the Path loads.
            //myBorder.Loaded += delegate (object sender, RoutedEventArgs e)
            //{
            //    ellipseStoryboard.Begin(this);
            //};

            ////StackPanel myStackPanel = new StackPanel();
            ////myStackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            //MiniStackPanel.Children.Add(myBorder);

            //MiniStackPanel = myStackPanel;
        }

        //private Control GetControlElement(InputControlType controlType, object selectedItem)
        //{
        //    switch (controlType)
        //    {
        //        case InputControlType.None:
        //            break;
        //        case InputControlType.checkbox:
        //            break;
        //        case InputControlType.Textbox:
        //            break;
        //        case InputControlType.Dropdown:
        //            break;
        //        case InputControlType.Button:
        //            return selectedItem as Control;

        //        case InputControlType.Image:
        //            break;
        //        case InputControlType.Number:
        //            break;
        //        case InputControlType.Password:
        //            break;
        //        case InputControlType.text:
        //            break;
        //        case InputControlType.submit:
        //            break;
        //        case InputControlType.TextBlock:
        //            return selectedItem as UIElement;
        //        default:
        //            return selectedItem as Control;
        //    }
        //    return selectedItem as Control;
        //}



        #endregion

        private void btnSaveDraft_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveDraft.mini_name = txtMiniName.Text;
                SaveDraft.accountid = "1234";
                SaveDraft.version = "1";
                

                List<SaveDraft.MiniPage> lmp = new List<SaveDraft.MiniPage>();
                SaveDraft.MiniPage mp = new SaveDraft.MiniPage();

                List<Controls> control = new List<Controls>();
                
                foreach (FrameworkElement element in MiniStackPanel.Children)
                
                {

                    Controls control1 = new Controls();
                    control1.name = element.Name;
                    control1.text = element.Tag.ToString();
                    control1.type = element.GetType().Name;
                    control1.placeholder = "";
                    control1.value = "";
                    control1.mapped_control = "";
                    control.Add(control1);
                }

                mp.controls = control;
                mp.page_name = "";
                mp.default_page = "";
                lmp.Add(mp);
                SaveDraft.mini_pages = lmp;
                string JsonText = Newtonsoft.Json.JsonConvert.SerializeObject(SaveDraft);
                System.IO.File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "MiniJson.json", JsonText);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
