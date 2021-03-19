<div id="config_edit">
	<div class="well sidebar-nav">
		<?php echo form_open("admin/conf_save/$name");?>
		<table border="0" width="400" cellspacing="4">
			<tr>
				<td align="left"><b>Параметр <?php echo $name; ?>:</b></td>
				<td align="right"><?php echo form_input('value',$value);?></td>
			</tr>
			<td></td><tr><td align="right" colspan=2 ><?php echo form_submit('submit', 'Сохранить');?></td></tr>
		</table>
		<?php echo form_close();?>
	</div>
</div>