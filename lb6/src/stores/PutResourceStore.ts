import { makeAutoObservable} from "mobx";
import * as resourcesApi from "../api/modules/resources";


class PutStore {
    updatedAt = "";

    constructor() {
        makeAutoObservable(this);

    }

    async update(id:string, name: string, color: string, year:string, pantone_value:string) {
        const result = await resourcesApi.updateResource({id, name, color, year, pantone_value});
        this.updatedAt = result.updatedAt;
        console.log(result);
    }
}

export default PutStore;