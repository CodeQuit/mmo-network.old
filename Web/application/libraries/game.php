<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');
class game
{
	/**
	* CodeIgniter global
	*
	* @var string
	**/
	protected $ci;

	public function __construct()
	{
		$this->ci =& get_instance();
		$this->ci->lang->load('news');
		$this->ci->load->model('game_model');
	}
	
	public function getGameList($login)
	{
		$game_list = $this->ci->game_model->game_list_model($login);
		return $game_list;
	}
	
	public function CreateAccount($login, $password, $gameid, $status)
	{
		$account_create = $this->ci->game_model->create_account_model($login, $password, $gameid, $status);
		return $account_create;
	}
	
	public function getStatusServer()
	{
		$game_status = $this->ci->game_model->status_server_model($login);
		return $game_status;
	}

}