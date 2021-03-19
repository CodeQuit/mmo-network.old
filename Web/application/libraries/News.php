<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');
/**
 * @author Andrey V. Ponomarenko
 * @date 03.05.2012-12:50:09
 */
 
class News {
	
	 /**
     * CodeIgniter global
     *
     * @var string
     **/
    protected $ci;
    
     /**
     * __construct
     *
     * @return void
     * @author Andrey V. Ponomarenko
     **/
	public function __construct(){
        $this->ci =& get_instance();
        $this->ci->lang->load('news');
        $this->ci->load->model('news_model');
        $this->ci->news = $this;
        $this->ci->news_model->trigger_events('library_constructor');
    }
    
    /**
     * __call
     *
     * Acts as a simple way to call model methods without loads of stupid alias'
     *
     **/
    public function __call($method, $arguments)
    {
        if (!method_exists( $this->ci->news_model, $method) )
        {
            throw new Exception('Undefined method News::' . $method . '() called');
        }
        return call_user_func_array( array($this->ci->news_model, $method), $arguments);
    }
    
    public function  getNews($count=null){
        $news_list = $this->ci->news_model->getNews($count);
        return $news_list;
    }
    
    public function  getNewsList($count=null){
        $news_list = $this->ci->news_model->getNewsList($count);
        return $news_list;
    }
    
    public function  getNewsID($id){
        $news_list = $this->ci->news_model->getNewsID($id);
        return $news_list;
    }

}