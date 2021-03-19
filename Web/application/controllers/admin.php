<?php defined('BASEPATH') OR exit('No direct script access allowed');

class admin extends CI_Controller
{

	function __construct()
	{
		parent::__construct();
		$this->load->library('logger');
		$this->load->library('config_sql');
		$this->load->library('launcher_lib');
		$this->load->library('services');
		$this->load->library('javascript');
		$this->load->library('ion_auth');
		$this->load->library('news');
		$this->load->library('session');
		$this->load->library('form_validation');
		$this->load->database();
		$this->load->helper('url');
	}

	function index()
	{
		$data['version'] = $this->config_sql->config_load('version');
		$this->load->view('body', $data);
		if($this->ion_auth->logged_in() && $this->ion_auth->is_admin())
		{

			$this->load->view('admin/welcome_admin_page');

		}
		else
		{
			$this->load->view('admin/noaccess', $data);
		}
		$this->load->view('footer', $data);
	}

	function configuration_manager()
	{
		$data['version'] = $this->config_sql->config_load('version');
		$this->load->view('body', $data);
		$data['configuration'] = $this->config_sql->config_load_all();
		if($this->ion_auth->logged_in() && $this->ion_auth->is_admin())
		{
			$this->load->view('admin/configuration_manager_view',$data);
		}
		else
		{
			$this->load->view('admin/noaccess', $data);
		}
		$this->load->view('footer', $data);
	}

	function conf_edit($name)
	{
		$data['version'] = $this->config_sql->config_load('version');
		$this->load->view('body', $data);
		$data['name'] = $name;
		$data['value'] = $this->config_sql->config_load($name);
		$this->load->view('admin/config_edit_view', $data);
		$this->load->view('footer', $data);
	}

	function conf_save($name)
	{
		$data['value'] = $this->input->post('value');
		$data['name'] = $name;
		$ret = $this->config_sql->config_save($data);
		redirect('/admin/configuration_manager', 'refresh' ,'3');
	}

	/*
 	* Список новостей сервисов в админке
 	*/
	function sNews()
	{
		$data['version'] = $this->config_sql->config_load('version');
		$this->load->view('body', $data);
		if($this->ion_auth->logged_in() && $this->ion_auth->is_admin())
		{
			$data['news_services'] = $this->services->getNews()->result();

			$this->load->view('admin/sNews_view', $data);
		}
		else
		{
			$this->load->view('admin/noaccess', $data);
		}
		$this->load->view('footer', $data);
	}

	public function sNewsADD()
	{
		$data['version'] = $this->config_sql->config_load('version');
		$this->load->view('body', $data);
		if($this->ion_auth->logged_in() && $this->ion_auth->is_admin())
		{
            $this->load->view('admin/sNewsADD_view', $data);
		}
		else
		{
			$this->load->view('admin/noaccess', $data);
		}
		$this->load->view('footer', $data);
	}


	public function sNewsEDIT($id)
	{
		$data['version'] = $this->config_sql->config_load('version');
		$this->load->view('body', $data);
		$data = $this->services->getNewsID($id)->row();
		if($this->ion_auth->logged_in() && $this->ion_auth->is_admin())
		{
            $this->load->view('admin/sNewsEDIT_view', $data);
		}
		else
		{
			$this->load->view('admin/noaccess', $data);
		}
		$this->load->view('footer', $data);
	}

	public function sNewsSAVE()
	{
		$data['version'] = $this->config_sql->config_load('version');
	    $this->load->view('body', $data);
	    $login_log = $Data_new->username;
        if($this->ion_auth->logged_in() && $this->ion_auth->is_admin()){
            $text = $this->input->post('text');
            $gameid = $this->input->post('gameid');
            $this->services->add($text, $gameid);
            $this->logger->writter($login_log, "Администратор добавил сервисную новость <b>#".$this->input->post('title')."</b>");
            echo '>>>Saving...';
            redirect('/admin/sNews/', 'refresh');
        }
        else
        {
            $this->load->view('admin/noaccess', $data);
        }
        $this->load->view('footer', $data);
	}

	public function sNewsUPDATE()
	{
		if($this->ion_auth->logged_in() && $this->ion_auth->is_admin()){
			$text = $this->input->post('text');
        	$gameid = $this->input->post('gameid');
        	$this->services->news_update($text, $gameid);
        	echo '>>>UPDATING...';
        	redirect('/admin/sNews/', 'refresh');
		}
		else
        {
            $this->load->view('admin/noaccess', $data);
        }
	}

	function userlist()
	{
		$data['version'] = $this->config_sql->config_load('version');
		$this->load->view('body', $data);
		if($this->ion_auth->logged_in() && $this->ion_auth->is_admin())
		{
			//set the flash data error message if there is one
			$this->data['message'] = (validation_errors()) ? validation_errors() : $this->session->flashdata('message');

			//list the users
			$this->data['users'] = $this->ion_auth->users()->result();
			foreach ($this->data['users'] as $k => $user)
			{
				$this->data['users'][$k]->groups = $this->ion_auth->get_users_groups($user->id)->result();
			}
			$this->load->view('admin/userlist', $this->data);
		}
		else
		{
			$this->load->view('admin/noaccess', $data);
		}
		$this->load->view('footer', $data);
		//$this->load->view('', $data_info);
	}

	public function editStatusServer()
	{
		$data['version'] = $this->config_sql->config_load('version');
		$this->load->view('body', $data);
		if($this->ion_auth->logged_in() && $this->ion_auth->is_admin())
		{
			$data[status] = $this->gamelib_model->getStatus();
			$this->load->view('admin/viewStatus', $data);
		}
		$this->load->view('footer', $data);
	}

	public function shlog($userlog)
	{
		$data['version'] = $this->config_sql->config_load('version');
		$this->load->view('body', $data);
		if($this->ion_auth->logged_in() && $this->ion_auth->is_admin())
		{
			$loggu['userslog'] = $this->logger->custom_log_reader($userlog);
			$loggu['user'] = $userlog;
			$this->load->view('admin/loglist', $loggu);
		}
		else
		{
			$this->load->view('admin/noaccess', $data);
		}
		$this->load->view('footer', $data);
	}

	public function account_info($userid)
	{
		$data['version'] = $this->config_sql->config_load('version');
		$this->load->view('body', $data);
		if($this->ion_auth->logged_in() && $this->ion_auth->is_admin())
		{
			$profile = $this->ion_auth->user($userid)->row();
			$profile->group= $this->ion_auth->get_users_groups($userid)->result();
			$profile->groups = $this->ion_auth->groups()->result();
			//var_dump($profile);
			$this->load->view('admin/account_info', $profile);
		}
		else
		{
			$this->load->view('admin/noaccess', $data);
		}
		$this->load->view('footer', $data);
	}

	/**
	 * Update user data
	 * @author Andrey V. Ponomarenko
	 */
	public function account_update(){
		$data['version'] = $this->config_sql->config_load('version');
	    if($this->ion_auth->logged_in() && $this->ion_auth->is_admin()){
	        $user_id = $this->input->post('user_id');
	        $old_group = $this->input->post('old_group');
	        $new_group = $this->input->post('group');
            if ($this->ion_auth->id_check($user_id)){
                $user = $this->ion_auth->user($user_id)->row();
                $data = array(
                    'active' => $this->input->post('active'),
                );
                $this->ion_auth->update($user_id,$data);
                $login_log = $Data_new->username;
                if ($old_group != $new_group){
                    $this->ion_auth->remove_from_group($old_group,$user_id);
                    $this->ion_auth->add_to_group($new_group,$user_id);
                }
                $this->logger->writter($login_log, "Администратор изменил данные пользователя <b>".$user->username."</b>");
                redirect('/admin/account_info/'.$user_id, 'refresh');
            }else{
                $this->session->set_flashdata('message', $this->ion_auth->errors());
                redirect('/admin/account_info/'.$user_id, 'refresh');
            }
        }else{
            $this->load->view('body', $data);
            $this->load->view('admin/noaccess', $data);
            $this->load->view('footer', $data);
        }
	}

     /**
     * News list
     * @author Andrey V. Ponomarenko
     */
	public function news(){
        $this->load->view('body', $data);
        if($this->ion_auth->logged_in() && $this->ion_auth->is_admin()){
        	$data['news'] = $this->news->getNewsList()->result();
            $this->load->view('admin/newslist', $data);
        }
        else
        {
            $this->load->view('admin/noaccess', $data);
        }
        $this->load->view('footer', $data);
	}

     /**
     * News edit
     * @author Andrey V. Ponomarenko
     */
    public function news_edit($id){
        $this->load->view('body', $data);
        if($this->ion_auth->logged_in() && $this->ion_auth->is_admin()){
            $data = $this->news->getNewsId($id)->row();

            $this->load->view('admin/newsedit', $data);
        }
        else
        {
            $this->load->view('admin/noaccess', $data);
        }
        $this->load->view('footer', $data);
    }

    /**
     * Update news
     * @author Andrey V. Ponomarenko
     */
    public function news_update(){
        if($this->ion_auth->logged_in() && $this->ion_auth->is_admin()){
            $id = $this->input->post('id');
            if ($this->news->id_check($id)){
                $data = array(
                    'date' => $this->input->post('date'),
                    'name' => $this->input->post('name'),
                    'title' => $this->input->post('title'),
                    'content' => $this->input->post('content'),
                );
                $this->news->update($id,$data);
                $this->logger->writter($login_log, "Администратор изменил данные новости <b>#".$id."</b>");
                redirect('/admin/news_edit/'.$id, 'refresh');
            }else{
                $this->session->set_flashdata('message', $this->ion_auth->errors());
                redirect('/admin/news_edit/'.$id, 'refresh');
            }
        }else{
            $this->load->view('body', $data);
            $this->load->view('admin/noaccess', $data);
            $this->load->view('footer', $data);
        }
    }

     /**
     * News add
     * @author Andrey V. Ponomarenko
     */
    public function news_add(){
        $this->load->view('body', $data);
        if($this->ion_auth->logged_in() && $this->ion_auth->is_admin()){
            $this->load->view('admin/newsadd', $data);
        }
        else
        {
            $this->load->view('admin/noaccess', $data);
        }
        $this->load->view('footer', $data);
    }

     /**
     * News add
     * @author Andrey V. Ponomarenko
     */
    public function news_added(){
        $this->load->view('body', $data);
        if($this->ion_auth->logged_in() && $this->ion_auth->is_admin()){
            $data = array(
                    'name' => $this->input->post('name'),
                    'title' => $this->input->post('title'),
                    'content' => $this->input->post('content'),
            );
            $this->news->add($data);
            $this->logger->writter($login_log, "Администратор добавил новость <b>#".$this->input->post('title')."</b>");
            redirect('/admin/news/', 'refresh');
        }
        else
        {
            $this->load->view('admin/noaccess', $data);
        }
        $this->load->view('footer', $data);
    }
}