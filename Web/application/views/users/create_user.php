<div class='mainInfo'>
	<div id="register_panel">
		<b>Пожалуйста заполните ниже поля, чтобы провести регистрацию нового аккаунта.</b>
		<div id="infoMessage"><?php echo $message;?></div>
		<?php echo form_open("users/create_user");?>
		<div id="register_block">
			<table border="0" width="400" cellspacing="4">
				<tr><td align="right">Логин:</td><td align="left"><?php echo form_input($username);?></td></tr>
				<tr><td align="right">Имя:</td><td align="left"><?php echo form_input($first_name);?></td></tr>
				<tr><td align="right">Фамилия:</td><td align="left"><?php echo form_input($last_name);?></td></tr>
				<tr><td align="right">Email:</td><td align="left"><?php echo form_input($email);?></td></tr>
				<tr><td align="right">Телефон:</td><td align="left"><?php echo form_input($phone1);?></td></tr>
				<tr><td align="right">Пароль:</td><td align="left"><?php echo form_input($password);?></td></tr>
				<tr><td align="right">Повторите пароль:</td><td align="left"><?php echo form_input($password_confirm);?></td></tr>
				<tr><td align="right"><?php echo form_submit('submit', 'Создать аккаунт');?></td></tr>
			</table>
		</div>
	</div>
	<?php echo form_close();?>
</div>