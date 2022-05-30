using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Data;
using System.Diagnostics;
using System.Windows.Navigation;
using System.Collections;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.Json;
using System.Windows.Threading;

namespace TEMA2
{
    public class MainVM : INotifyPropertyChanged
    {
        private Visibility playMenuVis;
        private Visibility loginMenuVis;
        private Visibility gameLostVis;
        private Visibility gameWonVis;
        private String infoBoxString;
        private String sequenceImage;
        private String underscoreWord;
        private String level;
        private String word;
        private String txtpath;
        private String usernameInput;
        private String statistic;
        private String category;
        private int timer;
        private int mistakes;
        private int selectedUser;
        private int selectedAvatar;
        private int levelNr;
        private bool gameOver;
        private char characterPressed;
        private User currentUser;
        private UserInput ui;
        private Statistics statWindow;
        private Categories categories;
        private About aboutWin;

        #region key sources strings
        private string q_source;
        private string w_source;
        private string e_source;
        private string r_source;
        private string t_source;
        private string y_source;
        private string u_source;
        private string i_source;
        private string o_source;
        private string p_source;
        private string a_source;
        private string s_source;
        private string d_source;
        private string f_source;
        private string g_source;
        private string h_source;
        private string j_source;
        private string k_source;
        private string l_source;
        private string z_source;
        private string x_source;
        private string c_source;
        private string v_source;
        private string b_source;
        private string n_source;
        private string m_source;
        #endregion
        #region avatar sources strings
        string avatar_0 = "/avatars/avatar_0.jpg";
        string avatar_1 = "/avatars/avatar_1.jpg";
        string avatar_2 = "/avatars/avatar_2.jpg";
        string avatar_3 = "/avatars/avatar_3.jpg";
        string avatar_4 = "/avatars/avatar_4.jpg";
        string avatar_5 = "/avatars/avatar_5.jpg";
        string avatar_6 = "/avatars/avatar_6.jpg";
        string avatar_7 = "/avatars/avatar_7.jpg";
        string avatar_8 = "/avatars/avatar_8.jpg";
        string avatar_9 = "/avatars/avatar_9.jpg";
        string avatar_10 = "/avatars/avatar_10.jpg";
        string avatar_11 = "/avatars/avatar_11.jpg";
        #endregion

        public List<char> wordToFind;
        public List<char> wrongCharacters;
        public ObservableCollection<char> underscore;
        public List<User> userList;

        public ObservableCollection<string> Usernames { get; set; } = new ObservableCollection<string>();

        private DateTime TimerStart { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region variables used for binding
        public Visibility PlayMenuVis
        {
            get { return playMenuVis; }
            set { playMenuVis = value; NotifyPropertyChanged("PlayMenuVis"); }
        }
        public Visibility LoginMenuVis
        {
            get { return loginMenuVis; }
            set { loginMenuVis = value; NotifyPropertyChanged("LoginMenuVis"); }
        }
        public Visibility GameLostVis
        {
            get { return gameLostVis; }
            set { gameLostVis = value; NotifyPropertyChanged("GameLostVis"); }
        }
        public Visibility GameWonVis
        {
            get { return gameWonVis; }
            set { gameWonVis = value; NotifyPropertyChanged("GameWonVis"); }
        }
        public String InfoBoxString
        {
            get { return infoBoxString; }
            set { infoBoxString = value; NotifyPropertyChanged("InfoBoxString"); }
        }
        public String SequenceImage
        {
            get { return sequenceImage; }
            set { sequenceImage = value; NotifyPropertyChanged("SequenceImage"); }
        }
        public String UnderscoreWord
        {
            get { return underscoreWord; }
            set { underscoreWord = value; NotifyPropertyChanged("UnderscoreWord"); }
        }
        public String Level
        {
            get { return level; }
            set { level = value; NotifyPropertyChanged("Level"); }
        }
        public String UsernameInput
        {
            get { return usernameInput; }
            set { usernameInput = value; NotifyPropertyChanged("UsernameInput"); }
        }
        public String Category
        {
            get { return category; }
            set { category = value; NotifyPropertyChanged("Category"); }
        }
        public String Statistic
        {
            get { return statistic; }
            set { statistic = value; NotifyPropertyChanged("Statistic"); }
        }
        public char CharacterPressed
        {
            get { return characterPressed; }
            set { characterPressed = value; NotifyPropertyChanged("CharacterPressed"); }
        }
        public int SelectedUser
        {
            get { return selectedUser; }
            set { selectedUser = value; if (userList.Count > 0 && SelectedUser >= 0) SelectedAvatar = userList[SelectedUser].AvatarIndex; NotifyPropertyChanged("SelectedUser"); }
        }
        public int SelectedAvatar
        {
            get { return selectedAvatar; }
            set { selectedAvatar = value; NotifyPropertyChanged("SelectedAvatar"); }
        }
        public int Timer
        {
            get { return timer; }
            set { timer = value; NotifyPropertyChanged("Timer"); }
        }
        public User CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; NotifyPropertyChanged("CurrentUser"); }

        }        
        #region key image sources
        public String Q_Source
        {
            get { return q_source; }
            set { q_source = value; NotifyPropertyChanged("Q_Source"); }
        }
        public String W_Source
        {
            get { return w_source; }
            set { w_source = value; NotifyPropertyChanged("W_Source"); }
        }
        public String E_Source
        {
            get { return e_source; }
            set { e_source = value; NotifyPropertyChanged("E_Source"); }
        }
        public String R_Source
        {
            get { return r_source; }
            set { r_source = value; NotifyPropertyChanged("R_Source"); }
        }
        public String T_Source
        {
            get { return t_source; }
            set { t_source = value; NotifyPropertyChanged("T_Source"); }
        }
        public String Y_Source
        {
            get { return y_source; }
            set { y_source = value; NotifyPropertyChanged("Y_Source"); }
        }
        public String U_Source
        {
            get { return u_source; }
            set { u_source = value; NotifyPropertyChanged("U_Source"); }
        }
        public String I_Source
        {
            get { return i_source; }
            set { i_source = value; NotifyPropertyChanged("I_Source"); }
        }
        public String O_Source
        {
            get { return o_source; }
            set { o_source = value; NotifyPropertyChanged("O_Source"); }
        }
        public String P_Source
        {
            get { return p_source; }
            set { p_source = value; NotifyPropertyChanged("P_Source"); }
        }
        public String A_Source
        {
            get { return a_source; }
            set { a_source = value; NotifyPropertyChanged("A_Source"); }
        }
        public String S_Source
        {
            get { return s_source; }
            set { s_source = value; NotifyPropertyChanged("S_Source"); }
        }
        public String D_Source
        {
            get { return d_source; }
            set { d_source = value; NotifyPropertyChanged("D_Source"); }
        }
        public String F_Source
        {
            get { return f_source; }
            set { f_source = value; NotifyPropertyChanged("F_Source"); }
        }
        public String G_Source
        {
            get { return g_source; }
            set { g_source = value; NotifyPropertyChanged("G_Source"); }
        }
        public String H_Source
        {
            get { return h_source; }
            set { h_source = value; NotifyPropertyChanged("H_Source"); }
        }
        public String J_Source
        {
            get { return j_source; }
            set { j_source = value; NotifyPropertyChanged("J_Source"); }
        }
        public String K_Source
        {
            get { return k_source; }
            set { k_source = value; NotifyPropertyChanged("K_Source"); }
        }
        public String L_Source
        {
            get { return l_source; }
            set { l_source = value; NotifyPropertyChanged("L_Source"); }
        }
        public String Z_Source
        {
            get { return z_source; }
            set { z_source = value; NotifyPropertyChanged("Z_Source"); }
        }
        public String X_Source
        {
            get { return x_source; }
            set { x_source = value; NotifyPropertyChanged("X_Source"); }
        }
        public String C_Source
        {
            get { return c_source; }
            set { c_source = value; NotifyPropertyChanged("C_Source"); }
        }
        public String V_Source
        {
            get { return v_source; }
            set { v_source = value; NotifyPropertyChanged("V_Source"); }
        }
        public String B_Source
        {
            get { return b_source; }
            set { b_source = value; NotifyPropertyChanged("B_Source"); }
        }
        public String N_Source
        {
            get { return n_source; }
            set { n_source = value; NotifyPropertyChanged("N_Source"); }
        }
        public String M_Source
        {
            get { return m_source; }
            set { m_source = value; NotifyPropertyChanged("M_Source"); }
        }
        #endregion

        #endregion

        private void SplitWord(String _word)
        {
            word = _word;
            wordToFind = new List<char>();
            underscore = new ObservableCollection<char>();
            wrongCharacters = new List<char>();
            word = word.ToUpper();
            for (int index = 0; index < word.Length; ++index)
            {
                wordToFind.Add(word[index]);
                if (word[index] != ' ')
                {
                    underscore.Add('_');
                    UnderscoreWord += '_';
                }
                else
                {
                    underscore.Add(' ');
                    UnderscoreWord += ' ';
                }
            }
        }

        private void ChangeButtonImg(char character, bool correct)
        {
            switch (character)
            {
                #region img source change
                case 'Q':
                    if (correct)
                        Q_Source = "icons/Q_KEY_GREEN.png";
                    else
                        Q_Source = "icons/Q_KEY_RED.png";
                    break;
                case 'W':
                    if (correct)
                        W_Source = "icons/W_KEY_GREEN.png";
                    else
                        W_Source = "icons/W_KEY_RED.png";
                    break;
                case 'E':
                    if (correct)
                        E_Source = "icons/E_KEY_GREEN.png";
                    else
                        E_Source = "icons/E_KEY_RED.png";
                    break;
                case 'R':
                    if (correct)
                        R_Source = "icons/R_KEY_GREEN.png";
                    else
                        R_Source = "icons/R_KEY_RED.png";
                    break;
                case 'T':
                    if (correct)
                        T_Source = "icons/T_KEY_GREEN.png";
                    else
                        T_Source = "icons/T_KEY_RED.png";
                    break;
                case 'Y':
                    if (correct)
                        Y_Source = "icons/Y_KEY_GREEN.png";
                    else
                        Y_Source = "icons/Y_KEY_RED.png";
                    break;
                case 'U':
                    if (correct)
                        U_Source = "icons/U_KEY_GREEN.png";
                    else
                        U_Source = "icons/U_KEY_RED.png";
                    break;
                case 'I':
                    if (correct)
                        I_Source = "icons/I_KEY_GREEN.png";
                    else
                        I_Source = "icons/I_KEY_RED.png";
                    break;
                case 'O':
                    if (correct)
                        O_Source = "icons/O_KEY_GREEN.png";
                    else
                        O_Source = "icons/O_KEY_RED.png";
                    break;
                case 'P':
                    if (correct)
                        P_Source = "icons/P_KEY_GREEN.png";
                    else
                        P_Source = "icons/P_KEY_RED.png";
                    break;
                case 'A':
                    if (correct)
                        A_Source = "icons/A_KEY_GREEN.png";
                    else
                        A_Source = "icons/A_KEY_RED.png";
                    break;
                case 'S':
                    if (correct)
                        S_Source = "icons/S_KEY_GREEN.png";
                    else
                        S_Source = "icons/S_KEY_RED.png";
                    break;
                case 'D':
                    if (correct)
                        D_Source = "icons/D_KEY_GREEN.png";
                    else
                        D_Source = "icons/D_KEY_RED.png";
                    break;
                case 'F':
                    if (correct)
                        F_Source = "icons/F_KEY_GREEN.png";
                    else
                        F_Source = "icons/F_KEY_RED.png";
                    break;
                case 'G':
                    if (correct)
                        G_Source = "icons/G_KEY_GREEN.png";
                    else
                        G_Source = "icons/G_KEY_RED.png";
                    break;
                case 'H':
                    if (correct)
                        H_Source = "icons/H_KEY_GREEN.png";
                    else
                        H_Source = "icons/H_KEY_RED.png";
                    break;
                case 'J':
                    if (correct)
                        J_Source = "icons/J_KEY_GREEN.png";
                    else
                        J_Source = "icons/J_KEY_RED.png";
                    break;
                case 'K':
                    if (correct)
                        K_Source = "icons/K_KEY_GREEN.png";
                    else
                        K_Source = "icons/K_KEY_RED.png";
                    break;
                case 'L':
                    if (correct)
                        L_Source = "icons/L_KEY_GREEN.png";
                    else
                        L_Source = "icons/L_KEY_RED.png";
                    break;
                case 'Z':
                    if (correct)
                        Z_Source = "icons/Z_KEY_GREEN.png";
                    else
                        Z_Source = "icons/Z_KEY_RED.png";
                    break;
                case 'X':
                    if (correct)
                        X_Source = "icons/X_KEY_GREEN.png";
                    else
                        X_Source = "icons/X_KEY_RED.png";
                    break;
                case 'C':
                    if (correct)
                        C_Source = "icons/C_KEY_GREEN.png";
                    else
                        C_Source = "icons/C_KEY_RED.png";
                    break;
                case 'V':
                    if (correct)
                        V_Source = "icons/V_KEY_GREEN.png";
                    else
                        V_Source = "icons/V_KEY_RED.png";
                    break;
                case 'B':
                    if (correct)
                        B_Source = "icons/B_KEY_GREEN.png";
                    else
                        B_Source = "icons/B_KEY_RED.png";
                    break;
                case 'N':
                    if (correct)
                        N_Source = "icons/N_KEY_GREEN.png";
                    else
                        N_Source = "icons/N_KEY_RED.png";
                    break;
                case 'M':
                    if (correct)
                        M_Source = "icons/M_KEY_GREEN.png";
                    else
                        M_Source = "icons/M_KEY_RED.png";
                    break;
                    #endregion
            }
        }

        private bool VerifyChar(char character)
        {
            bool found = false;
            UnderscoreWord = "";
            for (int index = 0; index < wordToFind.Count; ++index)
            {
                if (wordToFind[index] == character)
                {
                    underscore[index] = character;
                    found = true;
                }
                UnderscoreWord += underscore[index];
            }

            if (UnderscoreWord == word)
            {
                if (levelNr == 5)
                {
                    GameWonVis = Visibility.Visible;
                    gameOver = true;
                    ++CurrentUser.GamesWon;                       
                    SaveUserData();
                }
                else
                {
                    word = ChooseWord(txtpath);
                    UnderscoreWord = "";
                    //_word = "salut";
                    SplitWord(word);
                    TimerStart = DateTime.Now;
                    Timer = 30;
                }
                ++levelNr;
                if (levelNr <= 5)
                    Level = "Level " + levelNr.ToString();
                mistakes = 0;
                SequenceImage = "hangman_sequence/seq_0.jpg";
                #region setting key sources
                Q_Source = "icons/Q_KEY.png";
                W_Source = "icons/W_KEY.png";
                E_Source = "icons/E_KEY.png";
                R_Source = "icons/R_KEY.png";
                T_Source = "icons/T_KEY.png";
                Y_Source = "icons/Y_KEY.png";
                U_Source = "icons/U_KEY.png";
                I_Source = "icons/I_KEY.png";
                O_Source = "icons/O_KEY.png";
                P_Source = "icons/P_KEY.png";
                A_Source = "icons/A_KEY.png";
                S_Source = "icons/S_KEY.png";
                D_Source = "icons/D_KEY.png";
                F_Source = "icons/F_KEY.png";
                G_Source = "icons/G_KEY.png";
                H_Source = "icons/H_KEY.png";
                J_Source = "icons/J_KEY.png";
                K_Source = "icons/K_KEY.png";
                L_Source = "icons/L_KEY.png";
                Z_Source = "icons/Z_KEY.png";
                X_Source = "icons/X_KEY.png";
                C_Source = "icons/C_KEY.png";
                V_Source = "icons/V_KEY.png";
                B_Source = "icons/B_KEY.png";
                N_Source = "icons/N_KEY.png";
                M_Source = "icons/M_KEY.png";

                #endregion
            }

            if (found)
                return true;

            return false;
        }

        private String ChooseWord(String path)
        {
            int lineCount = File.ReadAllLines(path).Length;
            Random rand = new Random();
            int lineIndex = rand.Next(0, lineCount);
            string word = File.ReadLines(path).Skip(lineIndex - 1).Take(1).FirstOrDefault();
            return word;
        }

        private void ResetData()
        {
            GameLostVis = Visibility.Hidden;
            GameWonVis = Visibility.Hidden;
            mistakes = 0;
            gameOver = false;
            underscore.Clear();
            wordToFind.Clear();
            wrongCharacters.Clear();
            UnderscoreWord = "";
            SequenceImage = "hangman_sequence/seq_0.jpg";
            levelNr = 1;
            Level = "Level 1";
            //Timer = 30;
            TimerStart = DateTime.Now;
            #region setting key sources back to default
            Q_Source = "icons/Q_KEY.png";
            W_Source = "icons/W_KEY.png";
            E_Source = "icons/E_KEY.png";
            R_Source = "icons/R_KEY.png";
            T_Source = "icons/T_KEY.png";
            Y_Source = "icons/Y_KEY.png";
            U_Source = "icons/U_KEY.png";
            I_Source = "icons/I_KEY.png";
            O_Source = "icons/O_KEY.png";
            P_Source = "icons/P_KEY.png";
            A_Source = "icons/A_KEY.png";
            S_Source = "icons/S_KEY.png";
            D_Source = "icons/D_KEY.png";
            F_Source = "icons/F_KEY.png";
            G_Source = "icons/G_KEY.png";
            H_Source = "icons/H_KEY.png";
            J_Source = "icons/J_KEY.png";
            K_Source = "icons/K_KEY.png";
            L_Source = "icons/L_KEY.png";
            Z_Source = "icons/Z_KEY.png";
            X_Source = "icons/X_KEY.png";
            C_Source = "icons/C_KEY.png";
            V_Source = "icons/V_KEY.png";
            B_Source = "icons/B_KEY.png";
            N_Source = "icons/N_KEY.png";
            M_Source = "icons/M_KEY.png";
            #endregion
        }

        private void SaveUserData()
        {
            string[] lines = { CurrentUser.Username, CurrentUser.AvatarPath, CurrentUser.GamesPlayed.ToString(), CurrentUser.GamesWon.ToString(), SelectedAvatar.ToString() };
            File.WriteAllLines("../../users/" + CurrentUser.Username + ".txt", lines);
        }

        private void ReadData()
        {
            string[] files = Directory.GetFiles("../../users/");

            if (files.Length != 0)            
                for (int index = 0; index < files.Length; ++index)
                {
                    User newUser = new User();
                    newUser.Username = File.ReadLines(files[index]).Skip(0).Take(1).FirstOrDefault();
                    newUser.AvatarPath = File.ReadLines(files[index]).Skip(1).Take(1).FirstOrDefault();
                    newUser.GamesPlayed = Int16.Parse(File.ReadLines(files[index]).Skip(2).Take(1).FirstOrDefault());
                    newUser.GamesWon = Int16.Parse(File.ReadLines(files[index]).Skip(3).Take(1).FirstOrDefault());
                    newUser.AvatarIndex = Int16.Parse(File.ReadLines(files[index]).Skip(4).Take(1).FirstOrDefault());

                    userList.Add(newUser);
                    Usernames.Add(newUser.Username);
                }
        }

        private void dtTicker(object sender, EventArgs e)
        {
            if (Timer == 0)
            { 
                GameLostVis = Visibility.Visible;
                gameOver = true;
                UnderscoreWord = word;
            }
            else
            {
                var currentvalue = DateTime.Now - TimerStart;
                Timer = 30 - currentvalue.Seconds;                
            }                                     
        }

        public RelayCommand PlayCommand { get; set; }
        public RelayCommand ExitGameCommand { get; set; }
        public RelayCommand ExitAppCommand { get; set; }
        public RelayCommand KeyClickCommand { get; set; }
        public RelayCommand NewUserCommand { get; set; }
        public RelayCommand DeleteUserCommand { get; set; }
        public RelayCommand AboutCommand { get; set; }
        public RelayCommand SaveGameCommand { get; set; }
        public RelayCommand LoadGameCommand { get; set; }
        public RelayCommand RestartCommand { get; set; }
        public RelayCommand StatisticsCommand { get; set; }
        public RelayCommand ChangeCategoryCommand { get; set; }
        public RelayCommand SaveUsernameInputCommand { get; set; }
        public RelayCommand SaveCategoryCommand { get; set; }

        public MainVM()
        {
            Timer = 30;
            PlayMenuVis = Visibility.Hidden;
            LoginMenuVis = Visibility.Visible;
            userList = new List<User>();            
            txtpath = "all.txt";
            Category = "Category: all";
            ReadData();
            if (userList.Count != 0)
                SelectedAvatar = userList[SelectedUser].AvatarIndex;
            
            DispatcherTimer dt = new DispatcherTimer();
            dt.Tick += dtTicker;
            dt.Interval = new TimeSpan(0,0,0);           
            

            #region avatar list
            List<String> avatarPaths = new List<string>();            
            avatarPaths.Add(avatar_0);
            avatarPaths.Add(avatar_1);
            avatarPaths.Add(avatar_2);
            avatarPaths.Add(avatar_3);
            avatarPaths.Add(avatar_4);
            avatarPaths.Add(avatar_5);
            avatarPaths.Add(avatar_6);
            avatarPaths.Add(avatar_7);
            avatarPaths.Add(avatar_8);
            avatarPaths.Add(avatar_9);
            avatarPaths.Add(avatar_10);
            avatarPaths.Add(avatar_11);
            #endregion

            GameLostVis = Visibility.Hidden;
            GameWonVis = Visibility.Hidden;
            levelNr = 1;
            Level = "Level 1";
            InfoBoxString = "Choose who's playing:";
            SequenceImage = "hangman_sequence/seq_0.jpg";            
            int prevLevel = 1;           

            #region setting key sources
            Q_Source = "icons/Q_KEY.png";
            W_Source = "icons/W_KEY.png";
            E_Source = "icons/E_KEY.png";
            R_Source = "icons/R_KEY.png";
            T_Source = "icons/T_KEY.png";
            Y_Source = "icons/Y_KEY.png";
            U_Source = "icons/U_KEY.png";
            I_Source = "icons/I_KEY.png";
            O_Source = "icons/O_KEY.png";
            P_Source = "icons/P_KEY.png";
            A_Source = "icons/A_KEY.png";
            S_Source = "icons/S_KEY.png";
            D_Source = "icons/D_KEY.png";
            F_Source = "icons/F_KEY.png";
            G_Source = "icons/G_KEY.png";
            H_Source = "icons/H_KEY.png";
            J_Source = "icons/J_KEY.png";
            K_Source = "icons/K_KEY.png";
            L_Source = "icons/L_KEY.png";
            Z_Source = "icons/Z_KEY.png";
            X_Source = "icons/X_KEY.png";
            C_Source = "icons/C_KEY.png";
            V_Source = "icons/V_KEY.png";
            B_Source = "icons/B_KEY.png";
            N_Source = "icons/N_KEY.png";
            M_Source = "icons/M_KEY.png";

            #endregion

            PlayCommand = new RelayCommand(o =>
            {
                if (userList.Count > 0)
                {
                    LoginMenuVis = Visibility.Hidden;
                    PlayMenuVis = Visibility.Visible;
                    word = ChooseWord(txtpath);
                    //word = "salut";
                    SplitWord(word);
                    userList[SelectedUser].AvatarPath = avatarPaths[SelectedAvatar];
                    userList[SelectedUser].AvatarIndex = SelectedAvatar;
                    CurrentUser = userList[SelectedUser];
                    ++CurrentUser.GamesPlayed;
                    TimerStart = DateTime.Now;
                    dt.Start();
                }
            });

            KeyClickCommand = new RelayCommand(o =>
            {
                if (!gameOver && UnderscoreWord != "")
                {
                    char clickedChar = o.ToString()[0];
                    bool correct = VerifyChar(clickedChar);

                    if (!correct)
                    {
                        if (mistakes < 5 && !wrongCharacters.Contains(clickedChar))
                            ++mistakes;
                        wrongCharacters.Add(clickedChar);
                    }

                    if (mistakes == 5)
                    {                        
                        gameOver = true;
                        UnderscoreWord = word;
                        dt.Stop();
                        GameLostVis = Visibility.Visible;                        
                    }

                    SequenceImage = "hangman_sequence/seq_" + mistakes + ".jpg";

                    if (prevLevel == levelNr && levelNr != 6)
                        ChangeButtonImg(clickedChar, correct);

                    prevLevel = levelNr;
                }
            });

            ExitGameCommand = new RelayCommand(o =>
            {
                SaveUserData();
                ResetData();
                LoginMenuVis = Visibility.Visible;
                PlayMenuVis = Visibility.Hidden;
            });

            ExitAppCommand = new RelayCommand(o =>
            {
                Application.Current.Shutdown();
            });

            RestartCommand = new RelayCommand(o =>
            {
                ResetData();
                word = ChooseWord(txtpath);
                SplitWord(word);
                ++CurrentUser.GamesPlayed;                
                dt.Start();
            });

            NewUserCommand = new RelayCommand(o =>
            {
                ui = new UserInput(this);
                ui.Show();
            });

            DeleteUserCommand = new RelayCommand(o =>
            {
                if (userList.Count > 0)
                {
                    File.Delete("../../users/" + userList[SelectedUser].Username + ".txt");
                    int copy = SelectedUser;
                    Usernames.RemoveAt(SelectedUser);
                    userList.RemoveAt(copy);
                }
            });

            SaveUsernameInputCommand = new RelayCommand(o =>
            {
                // UsernameInput = ui.txtbox.Text.ToString();
                if (!Usernames.Contains(UsernameInput))
                {
                    User newUser = new User();
                    newUser.AvatarPath = avatarPaths[SelectedAvatar];
                    newUser.Username = UsernameInput;
                    newUser.GamesPlayed = 0;
                    newUser.GamesWon = 0;
                    newUser.AvatarIndex = 0;
                    Usernames.Add(UsernameInput);
                    userList.Add(newUser);
                    UsernameInput = "";
                    string[] lines = { newUser.Username, newUser.AvatarPath, newUser.GamesPlayed.ToString(), newUser.GamesWon.ToString(), newUser.AvatarIndex.ToString() };
                    File.WriteAllLines("../../users/" + newUser.Username + ".txt", lines);
                }                             
                ui.Close();
            });

            StatisticsCommand = new RelayCommand(o =>
            {
                string stats = "";
                for (int index = 0; index < userList.Count; ++index)
                {
                    User currUser = userList[index];
                    stats += currUser.Username + ": Games Played - " + currUser.GamesPlayed + "; Games Won - " + currUser.GamesWon + "; Win percentage: ";
                    if (currUser.GamesPlayed != 0)
                        stats += (currUser.GamesWon * 100 / currUser.GamesPlayed).ToString() + "%\n";
                    else
                        stats += "0%\n";                    
                }
                Statistic = stats;
                statWindow = new Statistics(this);
                if (userList.Count == 0)
                    Statistic = "No users registered.";
                statWindow.Show();
            });

            ChangeCategoryCommand = new RelayCommand(o =>
            {                
                categories = new Categories(this);
                categories.Show();
            });

            SaveCategoryCommand = new RelayCommand(o =>
            {
                ResetData();
                txtpath = o.ToString() + ".txt";
                Category = "Category: " + o.ToString();                
                word = ChooseWord(txtpath);
                SplitWord(word);
                categories.Close();
            });

            AboutCommand = new RelayCommand(o =>
            {
                aboutWin = new About();
                aboutWin.Show();
            });
        }
    }
}