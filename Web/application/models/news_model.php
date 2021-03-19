<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');
/**
 * @author Andrey V. Ponomarenko
 * @date 03.05.2012-12:54:43
 */
 
class News_model extends CI_Model{
	
	 /**
     * Holds an array of tables used
     *
     * @var string
     **/
    public $tables = 'news';

     /**
     * message (uses lang file)
     *
     * @var string
     **/
    protected $messages;

    /**
     * error message (uses lang file)
     *
     * @var string
     **/
    protected $errors;

    /**
     * error start delimiter
     *
     * @var string
     **/
    protected $error_start_delimiter;

    /**
     * error end delimiter
     *
     * @var string
     **/
    protected $error_end_delimiter;
    
     /**
     * Hooks
     *
     * @var object
     **/
    protected $_news_hooks;
    
    public function __construct(){
        parent::__construct();
        $this->load->database();
        $this->load->helper('date');
        //initialize our hooks object
        $this->_news_hooks = new stdClass;
        $this->trigger_events('model_constructor');
    }
    
    public function  getNews($count=null){
        if ($count) $limit = "LIMIT ".$count;
        $query = $this->db->query("SELECT * FROM  news ORDER BY date DESC ".$limit.";" );
        foreach ($query->result() as $row)
        {
            $data['date'] = $row->date;
            $data['name'] = $row->name;
            $data['title'] = $row->title;
            $data['content'] = $row->content;
            //echo '>>>notfound';
            $data_news = $data_news . "<b>$row->title</b><br>[Дата: $row->date | Автор:$row->name]<br>$row->content<br><br>";
        }
        return $data_news;
    }
    
    public function  getNewsList($count=null){
        if ($count) $limit = "LIMIT ".$count;
        $query = $this->db->query("SELECT * FROM  news ORDER BY date DESC ".$limit.";" );
        return $query;
    }
    
    public function  getNewsID($id){
        $query = $this->db->query("SELECT * FROM  news WHERE id = ".$id.";" );
        return $query;
    }
    
    /**
     * Checks ID
     *
     * @return bool
     * @author Andrey V. Ponomarenko
     **/
    public function id_check($id = ''){
        $this->trigger_events('id_check');
        if (empty($id)){
            return FALSE;
        }
        $this->trigger_events('extra_where');
        return $this->db->where('id', $id)
                        ->count_all_results($this->tables) > 0;
    }
    
    public function update($id, array $data){
        $this->trigger_events('pre_update_news');
        $this->db->update($this->tables, $data, array('id' => $id));
        if ($this->db->trans_status() === FALSE){
            $this->db->trans_rollback();
            $this->trigger_events(array('pre_update_news', 'post_update_news_unsuccessful'));
            $this->set_error('update_unsuccessful');
            return FALSE;
        }
        $this->db->trans_commit();
        $this->trigger_events(array('pre_update_news', 'post_update_news_successful'));
        $this->set_message('update_successful');
        return TRUE;
    }
    
    public function add($data){
    	$this->trigger_events('pre_add_news');
        $this->db->insert($this->tables, $data);
        $this->trigger_events('post_add_news');
    }
    
    public function set_hook($event, $name, $class, $method, $arguments){
        $this->_news_hooks->{$event}[$name] = new stdClass;
        $this->_news_hooks->{$event}[$name]->class     = $class;
        $this->_news_hooks->{$event}[$name]->method    = $method;
        $this->_news_hooks->{$event}[$name]->arguments = $arguments;
    }

    public function remove_hook($event, $name){
        if (isset($this->_news_hooks->{$event}[$name])){
            unset($this->_news_hooks->{$event}[$name]);
        }
    }

    public function remove_hooks($event){
        if (isset($this->_news_hooks->$event)){
            unset($this->_news_hooks->$event);
        }
    }

    protected function _call_hook($event, $name){
        if (isset($this->_news_hooks->{$event}[$name]) && method_exists($this->_news_hooks->{$event}[$name]->class, $this->_news_hooks->{$event}[$name]->method)){
            $hook = $this->_news_hooks->{$event}[$name];
            return call_user_func_array(array($hook->class, $hook->method), $hook->arguments);
        }
        return FALSE;
    }

    public function trigger_events($events){
        if (is_array($events) && !empty($events)){
            foreach ($events as $event){
                $this->trigger_events($event);
            }
        }else{
            if (isset($this->_news_hooks->$events) && !empty($this->_news_hooks->$events)){
                foreach ($this->_news_hooks->$events as $name => $hook){
                    $this->_call_hook($events, $name);
                }
            }
        }
    }
    

    public function row(){
        $this->trigger_events('row');
        $row = $this->response->row();
        $this->response->free_result();
        return $row;
    }

    public function row_array(){
        $this->trigger_events(array('row', 'row_array'));
        $row = $this->response->row_array();
        $this->response->free_result();
        return $row;
    }

    public function result(){
        $this->trigger_events('result');
        $result = $this->response->result();
        $this->response->free_result();
        return $result;
    }

    public function result_array(){
        $this->trigger_events(array('result', 'result_array'));
        $result = $this->response->result_array();
        $this->response->free_result();
        return $result;
    }

     /**
     * set_message
     *
     * Set a message
     *
     * @return void
     * @author Ben Edmunds
     **/
    public function set_message($message){
        $this->messages[] = $message;
        return $message;
    }

    /**
     * messages
     *
     * Get the messages
     *
     * @return void
     * @author Ben Edmunds
     **/
    public function messages(){
        $_output = '';
        foreach ($this->messages as $message){
            $messageLang = $this->lang->line($message) ? $this->lang->line($message) : '##' . $message . '##';
            $_output .= $this->message_start_delimiter . $messageLang . $this->message_end_delimiter;
        }

        return $_output;
    }

    /**
     * set_error
     *
     * Set an error message
     *
     * @return void
     * @author Ben Edmunds
     **/
    public function set_error($error)
    {
        $this->errors[] = $error;

        return $error;
    }

    /**
     * errors
     *
     * Get the error message
     *
     * @return void
     * @author Ben Edmunds
     **/
    public function errors()
    {
        $_output = '';
        foreach ($this->errors as $error)
        {
            $errorLang = $this->lang->line($error) ? $this->lang->line($error) : '##' . $error . '##';
            $_output .= $this->error_start_delimiter . $errorLang . $this->error_end_delimiter;
        }

        return $_output;
    }
}