#-*- coding:utf-8 –*-
import sqlite3,os,time
class sql(object):
	"""处理数据库的类"""
	def __init__(self, override):
		"""获取数据库连接"""
		super(sql, self).__init__()
		db = 'monsters.db'
		if os.path.exists(db):
			if override:
				os.remove(db)

		self.conn = sqlite3.connect(db)

		print "Open",db,"Success"
		if override:
			self.create_table_item()
			self.create_table_monster()
		
	def __del__(self):
		"""关闭数据库连接"""
		self.conn.close()

	def create_table_item(self):
		self.conn.execute('''CREATE TABLE ITEM(
			id INTEGER PRIMARY KEY,
			name TEXT NOT NULL,
			hp REAL NOT NULL,
			attack REAL NOT NULL,
			defence REAL NOT NULL,
			gold INT NOT NULL
		);''')

	def create_table_monster(self):
		"""创建数据库并创建数据表"""
		self.conn.execute('''CREATE TABLE MONSTER(
			id INTEGER PRIMARY KEY,
			name TEXT NOT NULL,
			hp REAL NOT NULL,
			attack REAL NOT NULL,
			defence REAL NOT NULL,
			gold INT NOT NULL
		);''')

	def show_table(self, table_name):
		"""显示表中所有数据"""
		if table_name == None:
			table_name = "None"
		table = self.execute("SELECT *  from " + table_name)
		if table != None:
			print table.fetchall()

	def has_id(self, id):
		if int(id) == 0:
			return False
		flag = self.execute("SELECT * from MONSTER WHERE id = " +  "'" + id + "'")
		for x in flag:
			return True
		return False

	def insert_monster(self, name, hp, attack, defence, gold):
		"""在MONSTER表中插入数据"""
		self.execute("""INSERT INTO MONSTER (id,name,hp,attack,defence,gold) \
			VALUES(NULL""" + ","+ "'" +name + "'" + "," + "'" + hp + "'" + ","+ "'" +attack + "'" + ","+ "'" +defence + "'" + ","+ "'" +gold + "'" +")"
			)
		self.conn.commit()

	def execute(self, seq):
		"""执行数据库语句"""
		# print seq
		return self.conn.execute(seq)

	def insert_monsters(self, monsters):
		for monster in monsters:
			monster = monster.replace("\n","").replace("\r","").replace("\t"," ")
			print monster
			a =  monster.split(" ")
			name = a[0]
			hp = float(a[1])
			if hp <= 0:
				hp = 1
			attack = float(a[2])
			if attack < 0:
				attack = 0
			defence = float(a[3])
			if defence > 100:
				defence = 100
			gold = float(a[4])
			if gold < 0:
				gold = 1
			self.insert_monster(name, str(hp), str(attack), str(defence), str(gold))

	def insert_items(self, items):
		for item in items:
			item = item.replace("\n","").replace("\r","").replace("\t"," ")
			print item
			a =  item.split(" ")
			name = a[0]
			hp = float(a[1])
			if hp < 0:
				hp = 0
			attack = float(a[2])
			if attack < 0:
				attack = 0
			defence = float(a[3])
			if defence > 100:
				defence = 100
			gold = float(a[4])
			if gold < 0:
				gold = 1
			self.insert_item(name, str(hp), str(attack), str(defence), str(gold))

	def insert_item(self, name, hp, attack, defence, gold):
		"""在ITEM表中插入数据"""
		self.execute("""INSERT INTO ITEM (id,name,hp,attack,defence,gold) \
			VALUES(NULL""" + ","+ "'" +name + "'" + "," + "'" + hp + "'" + ","+ "'" +attack + "'" + ","+ "'" +defence + "'" + ","+ "'" +gold + "'" +")"
			)
		self.conn.commit()

if __name__ == '__main__':
	"""创建怪物表"""
	s = sql(False)
	# monsters = open("monsters.txt","r")
	# items = open("items.txt","r")
	# s.insert_monsters(monsters.readlines()[1:])
	# s.insert_items(items.readlines()[1:])
	s.show_table("ITEM")
	


