﻿//-----------------------------------------------
//      Autor: Ramon Bollen
//       File: Challenge1.MainWindow.xaml.cs
// Created on: 2019927
//-----------------------------------------------

using Challenge1.ViewModels;

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Challenge1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;
        private readonly SolidColorBrush _black = new SolidColorBrush(Colors.Black);
        private readonly SolidColorBrush _gray = new SolidColorBrush(Colors.Gray);

        private readonly Dictionary<TextBox, PreviousState> _previousValues = new Dictionary<TextBox, PreviousState>();
        private readonly List<TextBox> _textboxes;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainWindowViewModel();
            DataContext = _viewModel;

            _textboxes = new List<TextBox>() { txtPlayerName, txtWidth, txtHeight, txtDifficulty };
            _textboxes.ForEach(tb =>
            {
                tb.Foreground = _gray;
                _previousValues.Add(tb, new PreviousState());

                tb.GotFocus += DefaultTextbox_Enter;
                tb.LostFocus += DefaultTextbox_Leave;
            });
        }

        private void DefaultTextbox_Enter(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;

            if (!string.IsNullOrEmpty(tb.Text))
            {
                _previousValues[tb].Text = tb.Text;
                _previousValues[tb].Color = tb.Foreground;
            }

            tb.Text = string.Empty;
            tb.Foreground = _black;
        }

        private void DefaultTextbox_Leave(object sender, EventArgs e)
        {
            var tb = (TextBox)sender;

            if (!string.IsNullOrEmpty(tb.Text.Trim())) { return; }
            // Lets get a new random player name just for fun
            if (tb == txtPlayerName)
            {
                tb.Text = MainWindowViewModel.RandomPlayer;
                tb.Foreground = _gray;
            }
            else if (tb != txtDifficulty)
            {
                tb.Text = _previousValues[tb].Text;
                tb.Foreground = _previousValues[tb].Color;
            }
        }

        private class PreviousState
        {
            public string Text { get; set; }
            public Brush Color { get; set; } = new SolidColorBrush(Colors.Gray);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txtRestInfo.Focus();
        }
    }
}