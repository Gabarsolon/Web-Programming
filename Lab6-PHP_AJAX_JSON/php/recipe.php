<?php
header("Access-Control-Allow-Origin: *");

class Recipe implements JsonSerializable {
	private $id;
	private $author;
	private $name;
	private $type;	
	private $prep_time;
    private $servings;
    private $ingredients;
    private $method;

	public function __construct($id, $author, $name, $type, $prep_time, $servings, $ingredients, $method) {
		$this->id = $id;
        $this->author = $author;
        $this->name = $name;
        $this->type = $type;
        $this->prep_time = $prep_time;
        $this->servings = $servings;
        $this->ingredients = $ingredients;
        $this->method = $method;
	}

	public function jsonSerialize() {
        $vars = get_object_vars($this);
        return $vars;
    }
}

?>
