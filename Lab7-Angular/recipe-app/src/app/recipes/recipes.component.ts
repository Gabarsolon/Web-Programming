import { Component, OnInit } from '@angular/core';
import { Recipe } from '../recipe';
import { GenericService } from '../generic.service';

@Component({
  selector: 'app-recipes',
  templateUrl: './recipes.component.html',
  styleUrls: ['./recipes.component.scss']
})
export class RecipesComponent implements OnInit{
  recipes: Recipe[] = [];
  displayedColumns: string[] = ['id', 'author', 'name', 'type', 'prep_time','servings', 'ingredients', 'method'];

  constructor(private genericService: GenericService){}

  ngOnInit(): void{
    console.log("ngOnInit called for StudentComponent");
    this.getRecipes();
  }

  getRecipes(): void {
      this.genericService.fetchRecipes()
        .subscribe(recipes => this.recipes = recipes);
  }
}
