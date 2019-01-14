//
//  UserPrefs.swift
//  Grocerly.iOS
//
//  Created by issd on 13/12/2018.
//  Copyright © 2018 SM71. All rights reserved.
//

import Foundation

class UserPrefs{
    let defaults = UserDefaults.standard
    
    func saveObject<T: Codable>(forKey: String,object: T, ofType _: T.Type ){
        do {
            let encoder = JSONEncoder()
            let json = try encoder.encode(object)
            defaults.set(json, forKey: forKey)
        } catch {
            print("saving did not work:\(error)")
        }
    }
    
    func loadObject<T: Codable>(forKey: String, ofType _: T.Type) -> T?{
        do {
            let json = defaults.data(forKey: forKey)
            let decoder = JSONDecoder()
            return try decoder.decode(T.self, from: json!)
        } catch {
            print("loading did not work:\(error)")
            return nil
        }
    }
}
