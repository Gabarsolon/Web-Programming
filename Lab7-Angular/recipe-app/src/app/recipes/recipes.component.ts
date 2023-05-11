import { Component, OnInit, Inject, ViewChild} from '@angular/core';
import { Recipe } from '../recipe';
import { GenericService } from '../generic.service';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Form, FormControl, FormGroup, NgForm, NgModel, Validators } from '@angular/forms';
import { MatInput } from '@angular/material/input';

@Component({
  selector: 'app-recipes',
  templateUrl: './recipes.component.html',
  styleUrls: ['./recipes.component.scss']
})
export class RecipesComponent implements OnInit{
  recipes: Recipe[] = [];
  displayedColumns: string[] = ['operations','id', 'author', 'name', 'type', 'prep_time','servings', 'ingredients', 'method'];
  type: string = '';
  recipe: Recipe = {
    id: 0,
    author: "",
    name: "",
    type: "",
    prep_time: "",
    servings: 1,
    ingredients: "",
    method: ""
  };

  constructor(private genericService: GenericService, public dialog: MatDialog){}

  ngOnInit(): void{
    // console.log("ngOnInit called for StudentComponent");
    this.getRecipes();
  }

  getRecipes(): void {
      this.genericService.fetchRecipes(this.type)
        .subscribe(recipes => this.recipes = recipes);
  }

  onFilter(): void{
    this.getRecipes();
  }

  onAdd(): void{
    const dialogRef = this.dialog.open(RecipeFormComponent, {
      data: this.recipe,
    });

    dialogRef.afterClosed().subscribe(ok => {
      if(ok){
        this.genericService.AddRecipe(this.recipe).subscribe(()=>{
          this.type="";
          this.getRecipes();
        });
       
      }
    });
  }

  onRemove(recipeId: number): void{
    const dialogRef = this.dialog.open(RemoveRecipeComponent, {
        data: recipeId
    });

    dialogRef.afterClosed().subscribe(ok => {
      if(ok){
        this.genericService.RemoveRecipe(recipeId).subscribe(()=>{
          this.type="";
          this.getRecipes();
        })
      }
    })
  }

  onUpdate(recipe: Recipe): void{
    const dialogRef = this.dialog.open(RecipeFormComponent, {
      data: recipe
    });

    dialogRef.afterClosed().subscribe(ok => {
      if(ok){
        this.genericService.UpdateRecipe(recipe).subscribe(()=>{
          this.type="";
          this.getRecipes();
        });
      }});
  }
}

@Component({
  selector: 'recipes.form',
  templateUrl: 'recipes.form.html',
  styleUrls: ['./recipes.component.scss']
})
export class RecipeFormComponent {
    name = new FormControl('', [Validators.required]);
    type = new FormControl('', [Validators.required]);
    servings = new FormControl('', [Validators.min(1)]);

  constructor(
    public dialogRef: MatDialogRef<RecipeFormComponent>,
    @Inject(MAT_DIALOG_DATA) public recipe: Recipe,
  ) {}

  onNoClick(): void {
    this.dialogRef.close();
  }
}

@Component({
  selector: 'recipes.remove-dialog',
  templateUrl: 'recipes.remove-dialog.html',
  styleUrls: ['./recipes.component.scss']
})
export class RemoveRecipeComponent {
  constructor(
    public dialogRef: MatDialogRef<RemoveRecipeComponent>,
    @Inject(MAT_DIALOG_DATA) public recipeId: number,
  ) {}

  onNoClick(): void {
    this.dialogRef.close();
  }
}