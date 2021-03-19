<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta name="description" content="" />
        <meta name="keywords" content="" />
        <title>MMO-NETWORK</title>
		<script type="text/javascript" src="http://code.jquery.com/jquery-1.7.2.js"></script>
		<script type="text/javascript" src="/application/views/styles/js/datatables.js"></script>
		<script type="text/javascript" src="/application/views/styles/js/bootstrap-dropdown.js"></script>
		<script type="text/javascript" src="/application/views/styles/js/bootstrap-modal.js"></script>
		<script type="text/javascript" src="/application/views/styles/js/prototype.js"></script>
		<script type="text/javascript" src="/application/views/styles/js/jquery.pjax.js"></script>
		<script type="text/javascript" src="/application/views/styles/js/system.js"></script>
		<style type="text/css" title="currentStyle">
			@import "/application/views/styles/css/bootstrap.css";
			@import "/application/views/styles/css/bootstrap-responsive.css";
			@import "/application/views/styles/css/general.css";
			@import "/application/views/styles/css/table.css";
		</style>
</head>
<body>
	<div class="navbar navbar-fixed-top">
		<div class="navbar-inner">
			<div class="container">
				<a class="brand" href="/">MMO-NETWORK</a>
				<ul class="nav pull-right">
					<li><a href="">Обновить</a></li>
					<li class="dropdown" id="game_menu">
						<a class="dropdown-toggle" data-toggle="dropdown" href="#game_menu">Наши игры<b class="caret"></b></a>
						<ul class="dropdown-menu">
							<li><a href="#">Lineage 2</a></li>
							<li><a href="http://forum.pbdev.ru/">Point Blank</a></li>
						</ul>
					</li>

					<?php if($this->ion_auth->logged_in() && $this->ion_auth->is_admin()){ ?>
					<li class="dropdown" id="admin_menu">
						<a class="dropdown-toggle" data-toggle="dropdown" href="#admin_menu">Админка<b class="caret"></b></a>
						<ul class="dropdown-menu">
							<li><a href="/admin">Главная страница</a></li>
							<li><a href="/admin/configuration_manager">Управление конфигурацией</a></li>
							<li><a href="/admin/sNews">Управление новостями сервисов</a></li>
							<li><a href="/admin/userlist">Управление пользователями</a></li>
							<li><a href="/admin/news">Управление новостями</a></li>
							<li><a href="/admin/shlog">Системные сообщения аккаунтов</a></li>
						</ul>
					</li>
					<?php } ?>

					<li class="dropdown" id="lk_menu">
						<a class="dropdown-toggle" data-toggle="dropdown" href="#lk_menu">Личный кабинет<b class="caret"></b></a>
						<ul class="dropdown-menu">
							<?php if(!$this->ion_auth->logged_in()) { ?>
								<li><a href="/" data-pjax='#main'>Главная страница</a></li>
								<li><a href="/client/MMoClientSetup.msi" data-pjax='#main'>Скачать клиент MMo</a></li>
								<li><a href="/users/create_user" data-pjax='#main'>Регистрация</a></li>
								<li><a href="/users/login" data-pjax='#main'>Войти</a></li>
							<?php } ?>

							<?php if($this->ion_auth->logged_in())
							{ ?>
								<li><a href="/" data-pjax='#main'>Главная страница</a></li>
								<li><a href="/client/MMoClientSetup.msi" data-pjax='#main'>Скачать клиент MMo</a></li>
								<li><a href="/panel" data-pjax='#main' >Панель</a></li>
								<li><a href="/users/change_password" data-pjax='#main' >Смена пароля</a></li>
								<li><a href="/panel/user_logs" data-pjax='#main' >Системные сообщения</a></li>
								<?php if($this->ion_auth->logged_in() && $this->ion_auth->is_admin()){ ?>
								<li><a href="/support" data-pjax='#main' >Служба поддержки</a></li>
								<?php } ?>
								<li><a href="/panel/profile" data-pjax='#main' >Профиль</a></li>
								<li><a href="/users/logout" data-pjax='#main' >Выйти</a></li>
							<?php } ?>
						</ul>
					</li>
				</ul>
			</div>
		</div>
	</div>
</body>
</html>