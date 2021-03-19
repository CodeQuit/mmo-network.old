                            <script type="text/javascript">
                            ( function($) {
                                $(document).ready( function() { 
                                    $('.table').dataTable({
                                        "sDom": 'fCl<"clear">rtip',
                                        "sPaginationType": "full_numbers",
                                         "aaSorting": [],
                                        "aoColumns": [ { "bSortable": false },null,null,null,null ]
                                    });
                                                                                 
                                });
                            } ) ( jQuery );
                            </script>
<div id="userlist">
    <div class="container"><a href='/admin/news_add/'>Добавить новость</a>
        <table class="table">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Дата</th>
                    <th>Автор</th>
                    <th>Заголовок</th>
                    <th>Управление</th>
                </tr>
            </thead>
            <tbody>
                <?php foreach ($news as $ns){?>
                <tr>
                    <td><?php echo $ns->id;?></td>
                    <td><?php echo $ns->date;?></td>
                    <td><?php echo $ns->name;?></td>
                    <td><?php echo $ns->title;?></td>
                    <td><?php echo "<a href='/admin/news_edit/$ns->id'>Редактировать</a>";?></td>
                </tr>
                <?php }?>
            </tbody>
        </table>
</div>