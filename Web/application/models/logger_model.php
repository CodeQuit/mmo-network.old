<?php  if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class logger_model extends CI_Model
{
	public function logs_writter_model($user_name, $text)
	{
		if($user_name != null && $text != null)
		{
			$query = $this->db->query("INSERT INTO logs_user (login_name, text) VALUES ('$user_name', '$text');");
		}
	}

	public function log_reader_model($user_name)
	{
		$query_new = $this->db->query("SELECT id, text, login_name, date FROM logs_user WHERE login_name='$user_name' ORDER BY date DESC LIMIT 30;");
		$data_logs = $data_logs . "<tr><td>Системный номер</td><td>Дата</td><td>Логин</td><td>Системное сообщение</td></tr>";
		foreach ($query_new->result() as $row)
		{
			$data_logs = $data_logs . "<tr><td>$row->id</td><td>$row->date</td><td>$row->login_name</td><td>$row->text</td></tr>";
		}
		return $data_logs;
	}

	public function custom_log_reader_model($nameuser = null, $limit = 500)
	{
		if($nameuser != null)
		{
			$query_new = $this->db->query("SELECT id, text, login_name, date FROM logs_user WHERE login_name='$nameuser' ORDER BY date DESC LIMIT $limit;");
		}
		else
		{	
			$query_new = $this->db->query("SELECT id, text, login_name, date FROM logs_user ORDER BY date DESC LIMIT $limit;");
		}

		$data_logs = $data_logs . "<tr><td>Системный номер</td><td>Дата</td><td>Логин</td><td>Системное сообщение</td></tr>";
		foreach ($query_new->result() as $row)
		{
			$data_logs = $data_logs . "<tr><td>$row->id</td><td>$row->date</td><td>$row->login_name</td><td>$row->text</td></tr>";
		}
		return $data_logs;
	}
}