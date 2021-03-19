<div id="forgot_password">
	<h1>Восстановление пароля</h1>
	<p>Пожалуйста введите свой email адрес и нажмите отправить, вам будет выслано присьмо с паролем.</p>
		<div id="infoMessage"><?php echo $message;?></div>
		<?php echo form_open("users/forgot_password");?>
      		<p>Email Address:<br />
			<?php echo form_input($email);?>
		</p>
		<p><?php echo form_submit('submit', 'Отправить');?></p>
</div>      
<?php echo form_close();?>