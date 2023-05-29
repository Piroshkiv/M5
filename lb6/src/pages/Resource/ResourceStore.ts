import {
    action, makeAutoObservable,
    makeObservable,
    observable,
    runInAction,
} from "mobx";

import * as userApi from "../../api/modules/resources";
import PutStore from "../../stores/PutResourceStore";




class UserStore {

    private putStore: PutStore;
    
    name = '';
    color = '';
    year = '';
    pantone_value = '';
    error = '';
    id = ''
    isLoading = false;
    updatedAt = '';

    constructor(putStore: PutStore) {
        this.putStore = putStore;
        makeAutoObservable(this);
    }

    changeName(name: string) {
        this.name = name;
        if (!!this.error) {
            this.error = '';
        }
    }

    changeColor(color: string) {
        this.color = color;
        if (!!this.error) {
            this.error = '';
        }
    }

    changeYear(year: string) {
        this.year = year;
        if (!!this.error) {
            this.error = '';
        }
    }
    
    changePantoneValue(pantone_value: string) {
        this.pantone_value = pantone_value;
        if (!!this.error) {
            this.error = '';
        }
    }
    async add() {
        try {
            this.isLoading = true;
           await this.putStore.update(this.id??'', this.name, this.color, this.year, this.pantone_value);
           this.updatedAt = this.putStore.updatedAt;
        }
        catch (e) {
            if (e instanceof Error) {
                this.error = e.message;
                
            }
        }
        this.isLoading = false;
    }
}

export default UserStore;