<div id="news_page">
<div class="well sidebar-nav">
<div id="infoMessage"><b><?php echo $message;?></b></div>
<?php echo form_open("admin/sNewsSAVE");?>
<table border="0" width="750" cellspacing="4">
    <tr>
        <td align="right"><b>номер сервиса:</b></td>
        <td align="left"><?php  echo form_input('gameid'); ?></td>
    </tr>
    <tr>
        <td align="right"><b>Текст новости(макс. 255):</b></td>
        <td align="left"><?php  echo form_textarea('text',null, 'class="news_content"'); ?></td>
    </tr>
    <tr><td align="right" colspan=2 ><?php echo form_submit('submit', 'Добавить новость');?></td></tr>
</table>
<?php echo form_close();?>
</div>
</div>
