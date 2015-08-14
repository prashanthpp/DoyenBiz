﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace DoyenBiz.CitizenKiosk.ViewModels
{
    public class MobileNumberViewModel : BaseViewModel
    {
        private ICommand m_KeyButtonCommand;

        public ICommand KeyButtonCommand
        {
            get
            {
                return m_KeyButtonCommand;
            }
            set
            {
                m_KeyButtonCommand = value;
            }
        }

        public TextBox InputBox
        {
            get;
            set;
        }

        public MobileNumberViewModel()
        {
            ButtonCommand = new RelayCommand(new Action<object>(GoButton_Click));
            KeyButtonCommand = new RelayCommand(new Action<object>(KeyButtonCommand_Click));
            Progress += 20;

        }

        public async void GoButton_Click(object inputBoxValue)
        {
            ToggleFlyout(0);
            ToggleFlyout(1);
        }

        public async void KeyButtonCommand_Click(object inputBoxValue)
        {
            if (inputBoxValue.ToString() != "backspace")
                InputBox.Text = InputBox.Text + inputBoxValue.ToString();
            else
            {
                if (!string.IsNullOrEmpty(InputBox.Text))
                {
                    InputBox.Text = InputBox.Text.Remove(InputBox.Text.Length - 1, 1);
                }
            }

        }
    }
}
