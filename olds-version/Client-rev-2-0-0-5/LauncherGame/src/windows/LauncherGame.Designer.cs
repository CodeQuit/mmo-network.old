namespace MMO_NETWORK_LAUNCHER
{
    partial class LauncherGame
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LauncherGame));
            this.game_1 = new System.Windows.Forms.Button();
            this.game_2 = new System.Windows.Forms.Button();
            this.game_4 = new System.Windows.Forms.Button();
            this.game_3 = new System.Windows.Forms.Button();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.start_game = new System.Windows.Forms.Button();
            this.trayicon = new System.Windows.Forms.NotifyIcon(this.components);
            this.localization_lang_05 = new System.Windows.Forms.Button();
            this.directoryEntry = new System.DirectoryServices.DirectoryEntry();
            this.FolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.player_name = new System.Windows.Forms.Label();
            this.localization_lang_23 = new System.Windows.Forms.Label();
            this.GameWait = new System.Windows.Forms.Panel();
            this.InfoUpdate_text = new System.Windows.Forms.Label();
            this.wait_text = new System.Windows.Forms.Label();
            this.TipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.localization_lang_28 = new System.Windows.Forms.Label();
            this.LGPoint = new System.Windows.Forms.Label();
            this.progressBar_FULL = new System.Windows.Forms.ProgressBar();
            this.Login_panel = new System.Windows.Forms.Panel();
            this.Register_link = new System.Windows.Forms.PictureBox();
            this.Enter_MMO = new System.Windows.Forms.PictureBox();
            this.status_update = new System.Windows.Forms.Label();
            this.Status_update_localize = new System.Windows.Forms.Label();
            this.Profilact = new System.Windows.Forms.Panel();
            this.localization_lang_06 = new System.Windows.Forms.Label();
            this.localization_lang_19 = new System.Windows.Forms.Label();
            this.localization_lang_29 = new System.Windows.Forms.Label();
            this.localization_lang_09 = new System.Windows.Forms.Label();
            this.pictureBoxUpdate = new System.Windows.Forms.PictureBox();
            this.Profpicture = new System.Windows.Forms.PictureBox();
            this.RememberBox = new System.Windows.Forms.CheckBox();
            this.localization_lang_14 = new System.Windows.Forms.TextBox();
            this.localization_lang_15 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.close_transp = new System.Windows.Forms.PictureBox();
            this.minimize_transp = new System.Windows.Forms.PictureBox();
            this.GameWait.SuspendLayout();
            this.Login_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Register_link)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Enter_MMO)).BeginInit();
            this.Profilact.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Profpicture)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.close_transp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimize_transp)).BeginInit();
            this.SuspendLayout();
            // 
            // game_1
            // 
            this.game_1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.game_1.Location = new System.Drawing.Point(1, 92);
            this.game_1.Name = "game_1";
            this.game_1.Size = new System.Drawing.Size(114, 41);
            this.game_1.TabIndex = 0;
            this.game_1.Text = "Aion";
            this.game_1.UseVisualStyleBackColor = true;
            this.game_1.Click += new System.EventHandler(this.GameButton_1_Click);
            // 
            // game_2
            // 
            this.game_2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.game_2.Location = new System.Drawing.Point(1, 139);
            this.game_2.Name = "game_2";
            this.game_2.Size = new System.Drawing.Size(114, 43);
            this.game_2.TabIndex = 1;
            this.game_2.Text = "Lineage 2";
            this.game_2.UseVisualStyleBackColor = true;
            this.game_2.Click += new System.EventHandler(this.GameButton_2_Click);
            // 
            // game_4
            // 
            this.game_4.Enabled = false;
            this.game_4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.game_4.Location = new System.Drawing.Point(2, 237);
            this.game_4.Name = "game_4";
            this.game_4.Size = new System.Drawing.Size(113, 43);
            this.game_4.TabIndex = 3;
            this.game_4.Text = "World of Warcraft";
            this.game_4.UseVisualStyleBackColor = true;
            this.game_4.Click += new System.EventHandler(this.GameButton_4_Click);
            // 
            // game_3
            // 
            this.game_3.Enabled = false;
            this.game_3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.game_3.Location = new System.Drawing.Point(2, 188);
            this.game_3.Name = "game_3";
            this.game_3.Size = new System.Drawing.Size(113, 43);
            this.game_3.TabIndex = 4;
            this.game_3.Text = "PointBlank";
            this.game_3.UseVisualStyleBackColor = true;
            this.game_3.Click += new System.EventHandler(this.GameButton_3_Click);
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(121, 92);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(567, 302);
            this.webBrowser.TabIndex = 5;
            this.webBrowser.Url = new System.Uri("", System.UriKind.Relative);
            // 
            // start_game
            // 
            this.start_game.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.start_game.Location = new System.Drawing.Point(681, 409);
            this.start_game.Name = "start_game";
            this.start_game.Size = new System.Drawing.Size(112, 42);
            this.start_game.TabIndex = 6;
            this.start_game.Text = "Start Game";
            this.start_game.UseVisualStyleBackColor = true;
            this.start_game.Click += new System.EventHandler(this.StartGame_Click);
            // 
            // trayicon
            // 
            this.trayicon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayicon.Icon")));
            this.trayicon.Text = "LG";
            this.trayicon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.trayicon_MouseDoubleClick);
            // 
            // localization_lang_05
            // 
            this.localization_lang_05.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.localization_lang_05.Location = new System.Drawing.Point(694, 92);
            this.localization_lang_05.Name = "localization_lang_05";
            this.localization_lang_05.Size = new System.Drawing.Size(79, 28);
            this.localization_lang_05.TabIndex = 16;
            this.localization_lang_05.Text = "_text";
            this.localization_lang_05.UseVisualStyleBackColor = true;
            this.localization_lang_05.Click += new System.EventHandler(this.PathToGame_Click);
            // 
            // player_name
            // 
            this.player_name.AutoSize = true;
            this.player_name.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.player_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.player_name.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.player_name.Location = new System.Drawing.Point(694, 133);
            this.player_name.Name = "player_name";
            this.player_name.Size = new System.Drawing.Size(77, 20);
            this.player_name.TabIndex = 34;
            this.player_name.Text = "NullString";
            this.player_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TipInfo.SetToolTip(this.player_name, "Ваш ник нейм в сети LauncherGame.");
            // 
            // localization_lang_23
            // 
            this.localization_lang_23.AutoSize = true;
            this.localization_lang_23.Cursor = System.Windows.Forms.Cursors.Hand;
            this.localization_lang_23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline);
            this.localization_lang_23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.localization_lang_23.Location = new System.Drawing.Point(715, 186);
            this.localization_lang_23.Name = "localization_lang_23";
            this.localization_lang_23.Size = new System.Drawing.Size(58, 20);
            this.localization_lang_23.TabIndex = 35;
            this.localization_lang_23.Text = "Выйти";
            this.TipInfo.SetToolTip(this.localization_lang_23, "Выход из системы LauncherGame.");
            this.localization_lang_23.Click += new System.EventHandler(this.Exit_Click);
            // 
            // GameWait
            // 
            this.GameWait.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.GameWait.Controls.Add(this.InfoUpdate_text);
            this.GameWait.Controls.Add(this.wait_text);
            this.GameWait.Location = new System.Drawing.Point(231, 188);
            this.GameWait.Name = "GameWait";
            this.GameWait.Size = new System.Drawing.Size(381, 107);
            this.GameWait.TabIndex = 45;
            this.GameWait.Visible = false;
            // 
            // InfoUpdate_text
            // 
            this.InfoUpdate_text.AutoSize = true;
            this.InfoUpdate_text.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.InfoUpdate_text.Location = new System.Drawing.Point(110, 52);
            this.InfoUpdate_text.Name = "InfoUpdate_text";
            this.InfoUpdate_text.Size = new System.Drawing.Size(177, 13);
            this.InfoUpdate_text.TabIndex = 45;
            this.InfoUpdate_text.Text = "Обновление информации об игре";
            // 
            // wait_text
            // 
            this.wait_text.AutoSize = true;
            this.wait_text.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.wait_text.Location = new System.Drawing.Point(125, 39);
            this.wait_text.Name = "wait_text";
            this.wait_text.Size = new System.Drawing.Size(136, 13);
            this.wait_text.TabIndex = 44;
            this.wait_text.Text = "Пожалуйста подождите...";
            this.wait_text.Visible = false;
            // 
            // TipInfo
            // 
            this.TipInfo.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.TipInfo.ToolTipTitle = "Информация";
            // 
            // localization_lang_28
            // 
            this.localization_lang_28.AutoSize = true;
            this.localization_lang_28.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.localization_lang_28.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.localization_lang_28.Location = new System.Drawing.Point(694, 153);
            this.localization_lang_28.Name = "localization_lang_28";
            this.localization_lang_28.Size = new System.Drawing.Size(62, 20);
            this.localization_lang_28.TabIndex = 42;
            this.localization_lang_28.Text = "Рубли: ";
            // 
            // LGPoint
            // 
            this.LGPoint.AutoSize = true;
            this.LGPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.LGPoint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LGPoint.Location = new System.Drawing.Point(775, 153);
            this.LGPoint.Name = "LGPoint";
            this.LGPoint.Size = new System.Drawing.Size(18, 20);
            this.LGPoint.TabIndex = 43;
            this.LGPoint.Text = "0";
            // 
            // progressBar_FULL
            // 
            this.progressBar_FULL.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.progressBar_FULL.Location = new System.Drawing.Point(104, 431);
            this.progressBar_FULL.Name = "progressBar_FULL";
            this.progressBar_FULL.Size = new System.Drawing.Size(560, 23);
            this.progressBar_FULL.TabIndex = 0;
            this.progressBar_FULL.Visible = false;
            // 
            // Login_panel
            // 
            this.Login_panel.BackColor = System.Drawing.Color.Transparent;
            this.Login_panel.BackgroundImage = global::MMO_NETWORK_LAUNCHER.Properties.Resources.Launcher_login;
            this.Login_panel.Controls.Add(this.Register_link);
            this.Login_panel.Controls.Add(this.Enter_MMO);
            this.Login_panel.Controls.Add(this.status_update);
            this.Login_panel.Controls.Add(this.Status_update_localize);
            this.Login_panel.Controls.Add(this.Profilact);
            this.Login_panel.Controls.Add(this.RememberBox);
            this.Login_panel.Controls.Add(this.localization_lang_14);
            this.Login_panel.Controls.Add(this.localization_lang_15);
            this.Login_panel.Location = new System.Drawing.Point(1, 38);
            this.Login_panel.Margin = new System.Windows.Forms.Padding(0);
            this.Login_panel.Name = "Login_panel";
            this.Login_panel.Size = new System.Drawing.Size(803, 17);
            this.Login_panel.TabIndex = 3;
            // 
            // Register_link
            // 
            this.Register_link.BackColor = System.Drawing.Color.Transparent;
            this.Register_link.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Register_link.Image = global::MMO_NETWORK_LAUNCHER.Properties.Resources.login_button_01;
            this.Register_link.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Register_link.Location = new System.Drawing.Point(251, 240);
            this.Register_link.Name = "Register_link";
            this.Register_link.Size = new System.Drawing.Size(172, 39);
            this.Register_link.TabIndex = 31;
            this.Register_link.TabStop = false;
            this.Register_link.Click += new System.EventHandler(this.Register_link_Click);
            // 
            // Enter_MMO
            // 
            this.Enter_MMO.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Enter_MMO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Enter_MMO.Image = global::MMO_NETWORK_LAUNCHER.Properties.Resources.login_button_02;
            this.Enter_MMO.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Enter_MMO.Location = new System.Drawing.Point(428, 33);
            this.Enter_MMO.Name = "Enter_MMO";
            this.Enter_MMO.Size = new System.Drawing.Size(130, 39);
            this.Enter_MMO.TabIndex = 30;
            this.Enter_MMO.TabStop = false;
            this.Enter_MMO.Click += new System.EventHandler(this.Enter_MMO_Click);
            // 
            // status_update
            // 
            this.status_update.AutoSize = true;
            this.status_update.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.status_update.Location = new System.Drawing.Point(176, 456);
            this.status_update.Name = "status_update";
            this.status_update.Size = new System.Drawing.Size(218, 13);
            this.status_update.TabIndex = 2;
            this.status_update.Text = "инициализация менеджера обновлений...";
            this.status_update.Visible = false;
            // 
            // Status_update_localize
            // 
            this.Status_update_localize.AutoSize = true;
            this.Status_update_localize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Status_update_localize.Location = new System.Drawing.Point(51, 456);
            this.Status_update_localize.Name = "Status_update_localize";
            this.Status_update_localize.Size = new System.Drawing.Size(110, 13);
            this.Status_update_localize.TabIndex = 1;
            this.Status_update_localize.Text = "Статус обновления: ";
            this.Status_update_localize.Visible = false;
            // 
            // Profilact
            // 
            this.Profilact.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Profilact.BackColor = System.Drawing.Color.White;
            this.Profilact.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Profilact.Controls.Add(this.localization_lang_06);
            this.Profilact.Controls.Add(this.localization_lang_19);
            this.Profilact.Controls.Add(this.localization_lang_29);
            this.Profilact.Controls.Add(this.localization_lang_09);
            this.Profilact.Controls.Add(this.pictureBoxUpdate);
            this.Profilact.Controls.Add(this.Profpicture);
            this.Profilact.Location = new System.Drawing.Point(10, -179);
            this.Profilact.Name = "Profilact";
            this.Profilact.Size = new System.Drawing.Size(58, 52);
            this.Profilact.TabIndex = 25;
            this.Profilact.Visible = false;
            // 
            // localization_lang_06
            // 
            this.localization_lang_06.AutoSize = true;
            this.localization_lang_06.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.localization_lang_06.Location = new System.Drawing.Point(274, 174);
            this.localization_lang_06.Name = "localization_lang_06";
            this.localization_lang_06.Size = new System.Drawing.Size(201, 52);
            this.localization_lang_06.TabIndex = 0;
            this.localization_lang_06.Text = "Уважаемые игроки, извините,\r\nСервер находится на профилактике...\r\nПриносим извине" +
                "ния за неудобства...\r\n\r\n";
            // 
            // localization_lang_19
            // 
            this.localization_lang_19.AutoSize = true;
            this.localization_lang_19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.localization_lang_19.Location = new System.Drawing.Point(100, 407);
            this.localization_lang_19.Name = "localization_lang_19";
            this.localization_lang_19.Size = new System.Drawing.Size(72, 13);
            this.localization_lang_19.TabIndex = 7;
            this.localization_lang_19.Text = "clientversion?";
            // 
            // localization_lang_29
            // 
            this.localization_lang_29.AutoSize = true;
            this.localization_lang_29.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.localization_lang_29.Location = new System.Drawing.Point(3, 407);
            this.localization_lang_29.Name = "localization_lang_29";
            this.localization_lang_29.Size = new System.Drawing.Size(91, 13);
            this.localization_lang_29.TabIndex = 6;
            this.localization_lang_29.Text = "Версия клиента:";
            // 
            // localization_lang_09
            // 
            this.localization_lang_09.AutoSize = true;
            this.localization_lang_09.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.localization_lang_09.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.localization_lang_09.Location = new System.Drawing.Point(234, 31);
            this.localization_lang_09.Name = "localization_lang_09";
            this.localization_lang_09.Size = new System.Drawing.Size(273, 25);
            this.localization_lang_09.TabIndex = 4;
            this.localization_lang_09.Text = "Технический перерыв...";
            // 
            // pictureBoxUpdate
            // 
            this.pictureBoxUpdate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxUpdate.Image = global::MMO_NETWORK_LAUNCHER.Properties.Resources.Refresh_48;
            this.pictureBoxUpdate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pictureBoxUpdate.Location = new System.Drawing.Point(374, -174);
            this.pictureBoxUpdate.Name = "pictureBoxUpdate";
            this.pictureBoxUpdate.Size = new System.Drawing.Size(47, 40);
            this.pictureBoxUpdate.TabIndex = 2;
            this.pictureBoxUpdate.TabStop = false;
            this.TipInfo.SetToolTip(this.pictureBoxUpdate, "Обновить страницу");
            this.pictureBoxUpdate.Click += new System.EventHandler(this.pictureBoxUpdate_Click_1);
            // 
            // Profpicture
            // 
            this.Profpicture.Image = global::MMO_NETWORK_LAUNCHER.Properties.Resources.kuvalda;
            this.Profpicture.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Profpicture.Location = new System.Drawing.Point(130, 171);
            this.Profpicture.Name = "Profpicture";
            this.Profpicture.Size = new System.Drawing.Size(100, 60);
            this.Profpicture.TabIndex = 1;
            this.Profpicture.TabStop = false;
            // 
            // RememberBox
            // 
            this.RememberBox.AutoSize = true;
            this.RememberBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.RememberBox.Location = new System.Drawing.Point(538, 455);
            this.RememberBox.Name = "RememberBox";
            this.RememberBox.Size = new System.Drawing.Size(111, 17);
            this.RememberBox.TabIndex = 29;
            this.RememberBox.Text = "Запомнить меня";
            this.RememberBox.UseVisualStyleBackColor = true;
            this.RememberBox.Visible = false;
            // 
            // localization_lang_14
            // 
            this.localization_lang_14.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.localization_lang_14.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.localization_lang_14.Location = new System.Drawing.Point(264, 149);
            this.localization_lang_14.MaxLength = 20;
            this.localization_lang_14.Name = "localization_lang_14";
            this.localization_lang_14.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.localization_lang_14.Size = new System.Drawing.Size(295, 22);
            this.localization_lang_14.TabIndex = 19;
            // 
            // localization_lang_15
            // 
            this.localization_lang_15.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.localization_lang_15.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.localization_lang_15.Location = new System.Drawing.Point(264, 198);
            this.localization_lang_15.MaxLength = 20;
            this.localization_lang_15.Name = "localization_lang_15";
            this.localization_lang_15.Size = new System.Drawing.Size(295, 22);
            this.localization_lang_15.TabIndex = 20;
            this.localization_lang_15.UseSystemPasswordChar = true;
            // 
            // panel1
            // 
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BackgroundImage = global::MMO_NETWORK_LAUNCHER.Properties.Resources.header_block;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.close_transp);
            this.panel1.Controls.Add(this.minimize_transp);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(803, 37);
            this.panel1.TabIndex = 46;
            // 
            // close_transp
            // 
            this.close_transp.BackColor = System.Drawing.Color.Transparent;
            this.close_transp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.close_transp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.close_transp.Location = new System.Drawing.Point(769, 11);
            this.close_transp.Name = "close_transp";
            this.close_transp.Size = new System.Drawing.Size(13, 16);
            this.close_transp.TabIndex = 33;
            this.close_transp.TabStop = false;
            this.close_transp.Click += new System.EventHandler(this.close_transp_Click);
            // 
            // minimize_transp
            // 
            this.minimize_transp.BackColor = System.Drawing.Color.Transparent;
            this.minimize_transp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.minimize_transp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.minimize_transp.Location = new System.Drawing.Point(748, 11);
            this.minimize_transp.Name = "minimize_transp";
            this.minimize_transp.Size = new System.Drawing.Size(15, 16);
            this.minimize_transp.TabIndex = 32;
            this.minimize_transp.TabStop = false;
            this.minimize_transp.Click += new System.EventHandler(this.minimize_transp_Click);
            // 
            // LauncherGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 465);
            this.Controls.Add(this.Login_panel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.progressBar_FULL);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.localization_lang_28);
            this.Controls.Add(this.LGPoint);
            this.Controls.Add(this.game_1);
            this.Controls.Add(this.localization_lang_05);
            this.Controls.Add(this.localization_lang_23);
            this.Controls.Add(this.player_name);
            this.Controls.Add(this.start_game);
            this.Controls.Add(this.game_3);
            this.Controls.Add(this.game_4);
            this.Controls.Add(this.game_2);
            this.Controls.Add(this.GameWait);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LauncherGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MMO-NETWORK";
            this.GameWait.ResumeLayout(false);
            this.GameWait.PerformLayout();
            this.Login_panel.ResumeLayout(false);
            this.Login_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Register_link)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Enter_MMO)).EndInit();
            this.Profilact.ResumeLayout(false);
            this.Profilact.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Profpicture)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.close_transp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimize_transp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button game_1;
        private System.Windows.Forms.Button game_2;
        private System.Windows.Forms.Button game_4;
        private System.Windows.Forms.Button game_3;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Button start_game;
        private System.Windows.Forms.NotifyIcon trayicon;
        private System.Windows.Forms.Button localization_lang_05;
        private System.DirectoryServices.DirectoryEntry directoryEntry;
        private System.Windows.Forms.FolderBrowserDialog FolderDialog;
        private System.Windows.Forms.Label player_name;
        private System.Windows.Forms.Label localization_lang_23;
        private System.Windows.Forms.ToolTip TipInfo;
        private System.Windows.Forms.Label localization_lang_28;
        private System.Windows.Forms.Label LGPoint;
        private System.Windows.Forms.Panel Login_panel;
        private System.Windows.Forms.TextBox localization_lang_14;
        private System.Windows.Forms.TextBox localization_lang_15;
        private System.Windows.Forms.Panel Profilact;
        private System.Windows.Forms.Label localization_lang_19;
        private System.Windows.Forms.Label localization_lang_29;
        private System.Windows.Forms.Label localization_lang_09;
        private System.Windows.Forms.PictureBox Profpicture;
        private System.Windows.Forms.Label localization_lang_06;
        private System.Windows.Forms.PictureBox pictureBoxUpdate;
        private System.Windows.Forms.ProgressBar progressBar_FULL;
        private System.Windows.Forms.Label status_update;
        private System.Windows.Forms.Label Status_update_localize;
        private System.Windows.Forms.CheckBox RememberBox;
        private System.Windows.Forms.Label wait_text;
        private System.Windows.Forms.Panel GameWait;
        private System.Windows.Forms.Label InfoUpdate_text;
        private System.Windows.Forms.PictureBox Enter_MMO;
        private System.Windows.Forms.PictureBox Register_link;
        private System.Windows.Forms.PictureBox close_transp;
        private System.Windows.Forms.PictureBox minimize_transp;
        private System.Windows.Forms.Panel panel1;
    }
}

