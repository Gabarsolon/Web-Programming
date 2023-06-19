<?php
header("Access-Control-Allow-Origin: *");

class Project implements JsonSerializable {
    private $id;
    private $project_manager_id;
    private $name;
    private $description;
    private $members;

    public function __construct($id, $project_manager_id, $name, $description, $members){
        $this->id = $id;
        $this->project_manager_id = $project_manager_id;
        $this->name = $name;
        $this->description = $description;
        $this->members = $members;
    }

    public function jsonSerialize() {
        $vars = get_object_vars($this);
        return $vars;
    }
}