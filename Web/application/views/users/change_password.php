<div id="infoMessage"><?php echo $message;?></div>
<div id="change_password">
<h1>Смена пароля.</h1><br>
<?php echo form_open("users/change_password");?>
	<p>Старый пароль:<br /><?php echo form_input($old_password);?></p>
	<p>Новый Пароль (не меннее <?php echo $min_password_length;?> символов):<br /><?php echo form_input($new_password);?></p>
	<p>Подтвердите новый пароль:<br /><?php echo form_input($new_password_confirm);?></p>
	<?php echo form_input($user_id);?>
	<p><?php echo form_submit('submit', 'Сменить');?></p>  
<?php echo form_close();?>
</div>
