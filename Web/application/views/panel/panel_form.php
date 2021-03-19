<div id="panel">
	<div id="game_list">
		<div class="well sidebar-nav">
			<div id="block_name"><b>Сервис аккаунты.</b></div>
			<div id="service_block">
				<ul class="nav nav-list" id="service_game">
					<?php echo $game_list; ?>
				</ul>
			</div>
		</div>
	</div>

	<!--- Статус блок --->
	<div id="right_block">
		<div class="well sidebar-nav">
			<div id="block_name"><b>Статус игровых серверов<br></b></div>
			<div id="status_block">
				<ul class="nav nav-list" id="status_game">
					<?php echo $status; ?>
				</ul>
			</div>
		</div>
	</div>
	<div id="content_page">
		<div id="system_info"><?php echo $sysinfo; ?></div>
		<div id="content_block_name"><b><center>Новости mmo-network.</center></b></div>
		<div id="news"><?php echo $news; ?></div>
	</div>
</div>