<div id="logs_user_view">
	<div id="block_name"></b>Системные сообщения <?php if($user != null) echo 'аккаунта ' . $user . '.'; else echo ' аккаунтов.'; ?></b></div>
	<div class="container">
		<table class="table">
			<tbody>		
				<?php echo $userslog; ?>
			</tbody>
		</table>
	</div>
</div>