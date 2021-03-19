<div class='mainInfo'>
	<div class="pageTitleBorder"></div>
	<?php echo form_open("users/login");?>
	<div id="login_panel">
		<p>Пожалуйста введите ваш логин или электроную почту и пароль для входа MMO-NETWORK.</p>
		<div id="infoMessage"><b><?php echo $message;?></b></div>
		<table border="0" width="350" cellspacing="4" style="margin: 0 auto;">
			<tr><td align="right">Логин:</td><td align="left"><?php echo form_input($identity);?></td></tr>
			<tr><td align="right">Пароль:</td><td align="left"><?php echo form_input($password);?></td></tr>
			<tr><td align="right">Запомнить меня:</td><td align="left"><?php echo form_checkbox('remember', '1', FALSE, 'id="remember"');?></td></tr>
			<tr><td align="right" colspan=2 ><?php echo form_submit('submit', 'Login');?></td></tr>
			<tr><td align="right" colspan=2><center><a href="forgot_password">Забыли пароль?</a></center></td></tr>
		</table>
	</div>
	<?php echo form_close();?>
</div>