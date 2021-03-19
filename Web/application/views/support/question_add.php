<div id="support">
<div id="question">
<div class="well sidebar-nav">
<?php echo form_open("support/questionAdded");?>
<table border="0" width="400" cellspacing="4">
    <tr>
        <td align="right"><b>Вопрос:</b></td>
        <td align="left"><?php echo form_textarea('question');?></td>
    </tr>
	<tr>
		<td align="right"><b>Игровой сервис:</b></td>
		<td align="left"><?php echo form_dropdown('services',$arr_name); ?></div>
	</tr>
    <tr><td></td><td align="right" colspan=1 ><?php echo form_submit('submit', 'Задать');?></td></tr>
</table>
<?php echo form_close();?>
</div>
</div>
</div>