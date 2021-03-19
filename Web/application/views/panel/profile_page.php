
<div id="profile_page">
<div class="well sidebar-nav">
<?php echo form_open("panel/profile_update");?>
<div id="infoMessage"><b><?php echo $message;?></b></div>
<input type="hidden" name="user_id" value="<?php  echo $id; ?>" />
<table border="0" width="400" cellspacing="4">
	<tr>
		<td align="right"><b>Имя:</b></td>
		<td align="left"><?php echo form_input('first_name',$first_name);?></td>
	</tr>
	<tr>
		<td align="right"><b>Фамилия:</b></td>
		<td align="left"><?php echo form_input('last_name',$last_name); ?></td>
	</tr>
	<tr>
		<td align="right"><b>Email:</b></td>
		<td align="left"><?php echo form_input('email',$email); ?></td>
	</tr>
	<tr>
		<td align="right"><b>Телефон:</b></td>
		<td align="left"><?php echo form_input('phone',$phone); ?></td>
	</tr>
	<tr>
		<td align="right" colspan=2><?php echo form_submit('submit', 'Update');?></td>
	</tr>
</table>
<?php echo form_close();?>
</div>
</div>
