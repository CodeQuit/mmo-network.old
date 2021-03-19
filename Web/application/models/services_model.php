<?php  if ( ! defined('BASEPATH')) exit('No direct script access allowed');
/*
 * @autor: DarkSkeleton
 */
class services_model extends CI_Model
{
	/*
	* Получить новость определеного сервиса с лимитом = 1
	*/
	public function getNews_model($id)
	{
		$this->load->database();
		$query = $this->db->query("SELECT id, text, gameid FROM service_news WHERE gameid='$id' LIMIT 1;");
		foreach ($query->result() as $row)
		{
			$data['text'] = $row->text;
			$data['gameid'] = $row->gameid;
		}
		return $data;
	}

	/*
	* Модель обновления новости
	*/
	public function news_update_model($text, $gameid)
	{
		$this->load->database();
		$query = $this->db->query("UPDATE service_news (text, gameid) VALUE ('$text', '$gameid');");
		echo '>>>' . $query;
	}

	/*
	* Добавить новость
	*/
	public function add_model($text, $gameid)
	{
		$this->load->database();
		$query = $this->db->query("INSERT INTO service_news (text, gameid) VALUE ('$text', '$gameid');");
		echo '>>>' . $query;
	}

	/*
	* Получить все новости
	*/
	public function getNews()
	{
		$query = $this->db->query("SELECT id, date, text, gameid FROM service_news;");
		return $query;
	}

	/*
	* Получение содержимого новости по ее id
	*/
	public function getNewsID($id)
	{
		$query = $this->db->query("SELECT id, date, text, gameid FROM service_news WHERE id='$id' LIMIT 1;");
		//echo $data['text'];
		return $query;
	}

	/*
	 * Получение имени игрового сервиса по номеру
	 */
	public function getName($gameid)
	{
		$query = $this->db->query("SELECT gameid, name FROM services WHERE gameid='$gameid' LIMIT 1;");
		$_name = $query->result();
		$name = $_name->name;
		return $name;
	}
	
	public function getAllName()
	{
		$query = $this->db->query("SELECT gameid, name FROM services;");
		$data['arr_name'] = array();
		foreach($query->result() as $row)
		{
			$data['arr_name'][] = $row->name;
		}
		return $data;
	}
}
?>