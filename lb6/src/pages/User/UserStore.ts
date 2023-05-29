import {
    action, makeAutoObservable,
    makeObservable,
    observable,
    runInAction,
} from "mobx";

import {IUser} from "../../interfaces/users";
import * as userApi from "../../api/modules/users";
import PutStore from "../../stores/PutStore";




class UserStore {

    private putStore: PutStore;
    
    firstName = '';
    secondName = '';
    image = ''
    email = '';
    error = '';
    id = ''
    isLoading = false;
    updatedAt = '';

    constructor(putStore: PutStore) {
        this.putStore = putStore;
        makeAutoObservable(this);
    }

    changeFirstName(name: string) {
        this.firstName = name;
        if (!!this.error) {
            this.error = '';
        }
    }

    changeSecondName(name: string) {
        this.secondName = name;
        if (!!this.error) {
            this.error = '';
        }
    }

    changeImage(image: string) {
        this.image = image;
        if (!!this.error) {
            this.error = '';
        }
    }

    changeEmail(email: string) {
        this.email = email;
        if (!!this.error) {
            this.error = '';
        }
    }

    async add() {
        try {
            this.isLoading = true;
           await this.putStore.update(this.id??'', this.firstName, this.secondName, this.image, this.email);
           this.updatedAt = this.putStore.updatedAt;
           console.log(this);
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