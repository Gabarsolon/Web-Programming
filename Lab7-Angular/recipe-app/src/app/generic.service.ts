import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { Recipe } from './recipe';

@Injectable({
  providedIn: 'root'
})
export class GenericService {
  private backendUrl = 'http://localhost/php/'

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private http: HttpClient) { }

  fetchRecipes(type: string) : Observable<Recipe[]>{
    return this.http.get<Recipe[]>(this.backendUrl+`get_recipes_by_type.php?type=${type}`)
      .pipe(catchError(this.handleError<Recipe[]>('fetchStudents', [])));
  }

   /**
  * Handle Http operation that failed.
  * Let the app continue.
  * @param operation - name of the operation that failed
  * @param result - optional value to return as the observable result
  */
   private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

    // TODO: send the error to remote logging infrastructure
    console.error(error); // log to console instead

    // Let the app keep running by returning an empty result.
    return of(result as T);
  };
} 
}
