import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'localize'
})
export class LocalizePipe implements PipeTransform {
    transform(value: string, ...args: any[]): string {
        // For now, just return the value. In a real app, this would handle translations
        return value;
    }
}
