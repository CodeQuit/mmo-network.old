<?php

class services
{
	/*
	 * Новостной сервис
	 * Конструктор
	 */
	function __construct()
	{
		$this->ci =& get_instance();
		$this->ci->load->model('services_model');
	}

	/*
	* Получить все новости
	*/
	public function getNews()
	{
		$all = $this->ci->services_model->getNews();
		return $all;
	}

	/*
	* Получить название сервиса
	*/
	public function getName($gameid)
	{
		$name = $this->ci->services_model->getName($gameid);
		return $name;
	}

	/*
 	* Добавить новость
 	*/
	public function add($text, $gameid)
	{
		$this->ci->services_model->add_model($text, $gameid);
	}

	/*
	* Обновить новость
	*/
	public function news_update($text, $gameid)
	{
		$this->ci->services_model->news_update_model($text, $gameid);
	}

	/*
	* Получить новость по ID
	*/
	public function getNewsID($id)
	{
		$data = $this->ci->services_model->getNewsID($id);
		return $data;
	}
	
	public function getAllName()
	{
		return $this->ci->services_model->getAllName();
	}
}
?>
