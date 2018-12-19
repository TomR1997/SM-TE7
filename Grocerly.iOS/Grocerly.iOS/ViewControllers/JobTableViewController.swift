//
//  JobTableViewController.swift
//  Grocerly.iOS
//
//  Created by issd on 29/11/2018.
//  Copyright © 2018 SM71. All rights reserved.
//

import UIKit

class JobTableViewController: UIViewController, UITableViewDataSource, UITableViewDelegate, DeleteRowDelegate {
    
    @IBOutlet weak var jobTable: UITableView!
    
    var shoppinglists = [ShoppingList]()
    var listIndex = 0

    override func viewDidLoad() {
        super.viewDidLoad()
        
        self.jobTable.dataSource = self
        self.jobTable.delegate = self
        
        mockShoppingLists()
    }

    func numberOfSections(in tableView: UITableView) -> Int {
        return 1
    }

    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return shoppinglists.count
    }

    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cellIdentifier = "JobCell"
        guard let cell = tableView.dequeueReusableCell(withIdentifier: cellIdentifier, for: indexPath) as? JobTableViewCell else {
            fatalError("Could not cast cell to jobcell")
        }
        
        let shoppinglist = shoppinglists[indexPath.row]
        
        cell.jobLabel.text = shoppinglist.name
        cell.avatarView.image = shoppinglist.image
        cell.distanceView.image = UIImage(named: "nearme")
        cell.itemsView.image = UIImage(named: "checklist")
        cell.itemsLabel.text = String(shoppinglist.shoppinglistItems)
        cell.distanceLabel.text = String(shoppinglist.distance) + " km"

        return cell
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        listIndex = indexPath.row
        performSegue(withIdentifier: "segue", sender: self)
    }
    
    override func prepare(for segue: UIStoryboardSegue, sender: Any?) {
        if (segue.identifier == "segue") {
            let itemViewController = segue.destination as! ItemViewController
            itemViewController.selectedShoppinglist = shoppinglists[listIndex]
            itemViewController.itemRowIndex = listIndex
            itemViewController.jobVC = self
        }
    }
    
    private func mockShoppingLists() {
        let shoppinglist1 = ShoppingList(name: "Mareike W's boodschappenlijst", id: "qwerty", image: UIImage(named: "mareike")!, distance: 2, shoppinglistItems: 1)
        let shoppinglist2 = ShoppingList(name: "Tom R's boodschappenlijst", id: "wertyu", image: UIImage(named: "tom")!, distance: 4, shoppinglistItems: 3)
        let shoppinglist3 = ShoppingList(name: "Sander Dl's boodschappenlijst", id: "rtyui", image: UIImage(named: "sander")!, distance: 7, shoppinglistItems: 2)
        
        shoppinglists += [shoppinglist1, shoppinglist2, shoppinglist3]
    }
    
    func deleteTableRow(index: Int){
        self.shoppinglists.remove(at: index)
        self.jobTable.reloadData()
    }

}
