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
    <div class="container"><a href='/admin/sNewsADD/'>Добавить новость</a>
        <table class="table">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Номер сервиса</th>
                    <th>Дата</th>
                    <th>Текст</th>
                    <th>Управление</th>
                </tr>
            </thead>
            <tbody>
                <?php foreach ($news_services as $ns){?>
                <tr>
		    <td><?php echo $ns->id;?></td>
                    <td><?php echo $ns->gameid;?></td>
                    <td><?php echo $ns->date;?></td>
                    <td><?php echo $ns->text;?></td>
                    <td><?php echo "<a href='/admin/sNewsEDIT/$ns->id'>Редактировать</a>";?></td>
                </tr>
                <?php }?>
            </tbody>
        </table>
</div>