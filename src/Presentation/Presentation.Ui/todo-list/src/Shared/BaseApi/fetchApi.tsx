import useAuth from "../../Clients/Authentication/useAuth";

//const baseUrl = process.env.REACT_APP_API_BASE_URL
const subscriptionKey = process.env.REACT_APP_API_SUBSCRIPTION_KEY as string

export class FetchApi {
  async apiCall<T>(endPoint: string, method: string, headers: Headers, content?: string): Promise<T> {
      const url_ = "https://localhost:7261" + endPoint
      let token =  GetToken()
      const bearer = `Bearer ${token}`;

      headers.append("Authorization", bearer);
      headers.append("SubscriptionKey", subscriptionKey);

      let options : RequestInit = {
          body: content,
          method: method,
          headers: headers
      };  

      return fetch(url_, options)
        .then((response: Response) => {
          if (!response.ok) {
            throw new Error(response.statusText)
          }
          return response.json() as Promise<T>
        })
        .then(data => {
          return data
        })
        .catch((error) => {
          throw error
        })
  }
}

const GetToken = async () : Promise<string> => {
  var { accessToken } = useAuth()
  if(!accessToken){
    return "Bearer "
  }
  else{
    return `Bearer ${accessToken}`
  }

}
 
