import { Component, OnInit } from '@angular/core';

import {Project} from './_models/project';
import {ProjectService} from './_services/project.service';
import {TaskService} from './_services/task.service';
import {ProjecteditorComponent} from './_components/projecteditor.component';
import {TaskeditorComponent} from './_components/taskeditor.component';
import {TreeModel} from 'angular2-tree-component';
import { Observable } from 'rxjs/Observable';
import { Token } from './_models/token';
import {TreeNode} from 'primeng/primeng';

import {SigninComponent} from './_components/signin.component';

@Component({
  moduleId: module.id,
  selector: 'my-app',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.css']
})

export class AppComponent implements OnInit{
  name = 'Bugtracker 1.0';
  projects: Project[];
  tasks: TreeNode[];
  filteredTasks: TreeNode[];
  filterStr: string;
  selectedProject: Project;
  hideProject = 1;
  hideEditor = 1;
  errorMessage: string;
  taskEditor = 0;
  usertoken: Token;
  selectedNode: TreeNode;
  selectedTask: TreeNode;
  isAddingChild: boolean;
  addingTaskId: number;
  
  constructor(
    private projectService: ProjectService,
    private taskService: TaskService
  ){}
  ngOnInit(): void {
    var currentUser = JSON.parse(localStorage.getItem('currentUser'));
    if (currentUser) {
      this.usertoken = currentUser;
      this.getProjects();
    }
    //this.getTasks(3);
  }
  
  getProjects(): void{
    this.projectService.getProjects(this.usertoken.access_token).subscribe(projects=>this.projects = projects, error =>  this.errorMessage = <any>error);
  }
  getTasks(projectId: number): void{
    this.taskService.getTasks(projectId).subscribe(tasks=>{ this.tasks = tasks; this.filteredTasks = tasks;});
    setTimeout(()=>{console.log(JSON.stringify(this.tasks));}, 1000);
  }
  filterTasks() {
    this.filteredTasks = [];
    for (let node of this.tasks) {
      if (this.checkIfContains(node))
        this.filteredTasks = [...this.filteredTasks, node];
    }
    console.log('filter!');
  }

  checkIfContains(node: TreeNode): boolean {
    if (JSON.stringify(node.data.title).indexOf(this.filterStr) >= 0 ||
        JSON.stringify(node.data.description).indexOf(this.filterStr) >= 0) return true;
    for (let child of node.children) {
      if (this.checkIfContains(child)) return true;
    }
    return false;
  }

  nodeSelect() {
    if (this.selectedNode == this.selectedTask) this.selectedNode = null;
    this.selectedTask = this.selectedNode;
  }

  selectProject(pr: Project): void{
    this.selectedProject = pr;
    if (pr.ParentId)
        this.selectedProject.ParentName = this.projects.find(p => p.Id == pr.ParentId).Name;
    this.getTasks(pr.Id);
  }

  openTaskEditor(edit: boolean): void{
    this.isAddingChild = false;
    this.addingTaskId = null;
    if (edit && !this.selectedTask) return;
    if (!edit && this.selectedTask) this.isAddingChild = true;
    if (this.selectedTask) this.addingTaskId = this.selectedTask.data.id;
    this.taskEditor = 1;
  }
  deleteTask(): void{
    this.taskService.deleteTask(this.selectedTask.data.id);
    setTimeout(()=>{this.getTasks(this.selectedProject.Id);}, 100);
  }
  
  hideProjects(): void{
    if(this.hideProject == 0){
      this.hideProject = 1;
    }else{
      this.hideProject = 0;
    }
  }
  hideEditors(): void{
    if(this.hideEditor == 0){
      this.hideEditor = 1;
    }else{
      this.hideEditor = 0;
    }
  }
  /*openProjectEditor(project: Project, rename: boolean): void{
    console.log("proj"+JSON.stringify(this.projects));
    if (!rename) {
      this.selectedProject = new Project();
      if (project != null) {
        this.selectedProject.ParentId = project.Id;
        this.selectedProject.ParentName = project.Name;
      }
    }
    else {
      this.selectedProject = project;
      if (project.ParentId)
        this.selectedProject.ParentName = this.projects.find(p => p.Id == project.ParentId).Name;
    }
    console.log("name " + this.selectedProject.Name);
    this.projectEditor = 1;
    this.taskEditor = 0;
  }
  renameProject(project: Project): void{
    console.log(''+name);
    this.openProjectEditor(project, true);
  }
  deleteProject(project: Project): void{
    this.projectService.deleteProject(project);
  }*/
  
  updateTasks(str: string): void {
    this.taskEditor = 0;
    setTimeout(()=>{this.getTasks(this.selectedProject.Id);}, 100);
  }

  setToken(token: Token) {
    this.usertoken = token;
    localStorage.setItem('currentUser', JSON.stringify(token));
    console.log(this.usertoken, this.usertoken.access_token);
    this.getProjects();
  }
}