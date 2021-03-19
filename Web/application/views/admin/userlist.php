							<script type="text/javascript">
							( function($) {
							    $(document).ready( function() { 
							    	$('.table').dataTable({
							    		"sDom": 'fCl<"clear">rtip',
							    		"sPaginationType": "full_numbers",
							    		 "aaSorting": [],
							    		"aoColumns": [ { "bSortable": false },null,null,null,null,null,null,null ]
							    	});
							    										    	 
								});
							} ) ( jQuery );
							</script>
<div id="userlist">
	<div class="container">
		<table class="table">
			<thead>
				<tr>
					<th>ID</th>
					<th>Логин</th>
					<th>Имя</th>
					<th>Фамилия</th>
					<th>Email</th>
					<th>Группа</th>
					<th>Статус</th>
					<th>Управление</th>
				</tr>
			</thead>
			<tbody>
				<?php foreach ($users as $user){?>
				<tr>
					<?php $user_temp =  $user->id ?>
					<td><?php echo $user_temp?></td>
					<td><?php echo "<a href='/admin/account_info/$user_temp'>$user->username</a>";?></td>
					<td><?php echo $user->first_name;?></td>
					<td><?php echo $user->last_name;?></td>
					<td><?php echo $user->email;?></td>
					<td>
						<?php foreach ($user->groups as $group):?>
							<?php echo $group->description;?><br />
						<?php endforeach?>
					</td>
					<td><?php echo ($user->active) ? 'Активен' : 'Заблокирован';?></td>
					<td><?php echo "<a href='/admin/account_info/$user_temp'>Редактировать</a>";?></td>
				</tr>
				<?php }?>
			</tbody>
		</table>
</div>