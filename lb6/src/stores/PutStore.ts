import { makeAutoObservable} from "mobx";
import * as usersApi from "../api/modules/users";
import {IUser} from "../interfaces/users";

class PutStore {
    updatedAt = "";

    constructor() {
        makeAutoObservable(this);

    }

    async update(id: string, first_name: string, second_name: string, img: string, email: string) {
        const result = await usersApi.updateUser({id, first_name, second_name, img, email});
        this.updatedAt = result.updatedAt;
        console.log(result);
    }
}

export default PutStore;