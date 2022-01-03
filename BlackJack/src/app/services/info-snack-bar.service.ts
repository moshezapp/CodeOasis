import { MatSnackBar } from '@angular/material/snack-bar';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';


@Injectable({
  providedIn: 'root'
})
export class InfoSnackBarService {
  constructor(private _snackBar: MatSnackBar) { }

  Error(message: string) {
    this._snackBar.open("⚠\t" + message, "OK", {
      duration: 5000,
      panelClass: ['blue-snackbar']
    });
  }

  Info(message: string) {
    this._snackBar.open("🛈\t" + message, "OK", {
      duration: 5000,
      panelClass: ['blue-snackbar']
    });
  }
}
