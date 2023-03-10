import { Pipe, PipeTransform } from "@angular/core";


@Pipe({name: 'dateOnly'})
export class DateOnlyPipe implements PipeTransform {
    transform(value: string): string {
        return value.split('T')[0];
    }
}