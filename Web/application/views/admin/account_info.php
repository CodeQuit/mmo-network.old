<div id="profile_page">
<div class="well sidebar-nav">
<div id="infoMessage"><b><?php echo $message;?></b></div>
<?php echo form_open("admin/account_update");?>
<input type="hidden" name="user_id" value="<?php  echo $id; ?>" />
<input type="hidden" name="old_group" value="<?php  echo $group[0]->id; ?>" />
<table border="0" width="400" cellspacing="4">
	<tr>
		<td align="right"><b>Логин:</b></td>
		<td align="left"><?php  echo $username; ?></td>
	</tr>
	<tr>
		<td align="right"><b>Имя:</b></td>
		<td align="left"><?php  echo $first_name; ?></td>
	</tr>
	<tr>
		<td align="right"><b>Фамилия:</b></td>
		<td align="left"><?php  echo $last_name; ?></td>
	</tr>
	<tr>
		<td align="right"><b>Email:</b></td>
		<td align="left"><?php  echo $email; ?></td>
	</tr>
	<tr>
		<td align="right"><b>Телефон:</b></td>
		<td align="left"><?php  echo $phone; ?></td>
	</tr>
    <tr>
        <td align="right"><b>Рубли:</b></td>
        <td align="left"><?php  echo $money; ?></td>
    </tr>
    <tr>
        <td align="right"><b>Группа:</b></td>
        <td align="left">
            <select name="group">
                <?php 
                foreach ($groups as $gr){
                    if ($group[0]->id == $gr->id) $selected = 'selected';
                    else $selected = '';
                ?>
                   <option value='<?php  echo $gr->id;?>' <?php  echo $selected;?> >
                       <?php  echo $gr->description;?>
                   </option>
                <?php }?>
            </select>
        </td>
    </tr>
    <tr>
        <td align="right"><b>Состояние:</b></td>
        <td align="left">
            <select name="active">
                <?php 
                    var_dump($active);
                    if ($active == 0) $selected1 = 'selected';
                    else $selected1 = '';
                    if ($active == 1) $selected2 = 'selected';
                    else $selected2 = '';
                ?>
                <option value='0' <?php  echo $selected1;?>>Заблокирован</option>
                <option value='1' <?php  echo $selected2;?>>Активен</option>
            </select>
         </td>
    </tr>
    <tr><td align="right" colspan=2 ><?php echo form_submit('submit', 'Update');?></td></tr>
</table>
<?php echo form_close();?>
<?php echo "<center><a href='/admin/shlog/$username'>Показать системные сообщения аккаунта.</a></center>"; ?>
</div>
</div>
