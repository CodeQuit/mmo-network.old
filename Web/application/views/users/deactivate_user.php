<div class='mainInfo'>

	<div class="pageTitle">Блокировка пользователя</div>
    <div class="pageTitleBorder"></div>
	<p>Вы точно уверены что хотите заблокировать пользователя '<?php echo $user->username; ?>'</p>
	
    <?php echo form_open("users/deactivate/".$user->id);?>
    	
      <p>
      	<label for="confirm">Да:</label>
		<input type="radio" name="confirm" value="yes" checked="checked" />
      	<label for="confirm">Нет:</label>
		<input type="radio" name="confirm" value="no" />
      </p>
      
      <?php echo form_hidden($csrf); ?>
      <?php echo form_hidden(array('id'=>$user->id)); ?>
      
      <p><?php echo form_submit('submit', 'Заблокировать');?></p>

    <?php echo form_close();?>

</div>
