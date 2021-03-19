<?php

class support_library
{
	/*
	 * Поддержка
	 * Конструктор
	 */
	function __construct()
	{
		$this->ci =& get_instance();
		$this->ci->load->model('support_model');
	}
	
	function getQuestion($username)
	{
		$all = $this->ci->support_model->getQuestion($username);
		return $all;
	}
	
	function addQuestion($question, $account, $status, $gameid)
	{
		$this->ci->support_model->addQuestion($question, $account, $status, $gameid);
	}
}
?>