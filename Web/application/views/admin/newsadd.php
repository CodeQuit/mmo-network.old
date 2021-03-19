<div id="news_page">
<div class="well sidebar-nav">
<div id="infoMessage"><b><?php echo $message;?></b></div>
<?php echo form_open("admin/news_added");?>
<table border="0" width="640" cellspacing="4">
    <tr>
        <td align="right"><b>Админ:</b></td>
        <td align="left"><?php echo form_input('name');?></td>
    </tr>
    <tr>
        <td align="right"><b>Заголовок:</b></td>
        <td align="left"><?php  echo form_input('title'); ?></td>
    </tr>
    <tr>
        <td align="right"><b>Описание:</b></td>
        <td align="left"><?php  echo form_textarea('content',null, 'class="news_content"'); ?></td>
    </tr>
    <tr><td align="right" colspan=2 ><?php echo form_submit('submit', 'New news');?></td></tr>
</table>
<?php echo form_close();?>
</div>
</div>
